using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float enginePower = 5.0f;
    public float rotationSpeed = 90.0f;
    public float reloadingTime = 4.0f;
    public int amoMax = 30;
    public int hitpointsMax = 5;
    public GameObject projectile;
    public GameObject rocket;

    private GameObject groundObj;
    private float vertical;
    private float horizontal;
    private float speed = 0.0f;
    private float reloadingTimer = 0.0f;
    private float currentEnginePower;
    private float currentRotationSpeed;
    private float engineBustTimer;
    private int hitpoints;
    private int amo;
    private int rocketsCount;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        // находим объект земли и по его коллайдеру рассчитываем ограничения для игрока
        groundObj = GameObject.Find("Ground");
        PlayerBoundsCalculation();
        currentEnginePower = enginePower;
        currentRotationSpeed = rotationSpeed;
        engineBustTimer = 0.0f;
        hitpoints = hitpointsMax;
        amo = amoMax;
        rocketsCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // двигаем игрока
        MovePlayer();

        reload();
        Debug.Log(reloadingTimer);

        if(Input.GetKeyDown(KeyCode.Space) && reloadingTimer == 0.0f)
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            LaunchRocket();
        }
    }

    private void PlayerBoundsCalculation()
    {
        minX = groundObj.transform.position.x - groundObj.GetComponent<BoxCollider>().size.x * 0.5f * groundObj.transform.localScale.x + 2.0f;
        maxX = groundObj.transform.position.x + groundObj.GetComponent<BoxCollider>().size.x * 0.5f * groundObj.transform.localScale.x - 2.0f;
        minZ = groundObj.transform.position.z - groundObj.GetComponent<BoxCollider>().size.z * 0.5f * groundObj.transform.localScale.z + 2.0f;
        maxZ = groundObj.transform.position.z + groundObj.GetComponent<BoxCollider>().size.z * 0.5f * groundObj.transform.localScale.z - 2.0f;
    }

    private void reload()
    {
        if (reloadingTimer > 0.0f)
        {
            reloadingTimer -= Time.deltaTime;
            if (reloadingTimer < 0.0f)
            {
                reloadingTimer = 0.0f;
            }
        }
    }

    private void LaunchRocket()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && rocketsCount > 0)
        {
            GameObject obj = Instantiate(rocket, transform.position + transform.forward * 2.0f, transform.rotation);
            rocketsCount--;
        }
    }

    private void Shoot()
    {
        if (amo > 0)
        {
            GameObject obj = Instantiate(projectile, transform.position + transform.forward * 2.0f, transform.rotation);
            reloadingTimer = reloadingTime;
            amo--;
        }
    }

    private void MovePlayer()
    {
        if (engineBustTimer > 0)
        {
            EngineBustDecreas();
        }

        // получаем инпут движения
        GetMovingInput();

        // рассчитываем изменение скорости от инпута
        SpeedCalculation();

        // если (после рассчета скорости) движется назад - инвертируем поворот
        if (speed < 0) horizontal *= -1;

        // поворачиваем и двигаем (с учетом ограничений)
        transform.Rotate(Vector3.up, horizontal * currentRotationSpeed * Time.deltaTime);
        Vector3 newPos = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, minX, maxX), newPos.y, Mathf.Clamp(newPos.z, minZ, maxZ));
    }

    private void EngineBustDecreas()
    {
        engineBustTimer -= Time.deltaTime;
        if(engineBustTimer < 0.0f)
        {
            currentEnginePower = enginePower;
            currentRotationSpeed = rotationSpeed;
        }
    }

    private void GetMovingInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void SpeedCalculation()
    {
        // идет накатом
        if (Mathf.Approximately(vertical, 0.0f))
        {
            // останавливаем, если скорость очень маленькая
            // иначе медленно уменьшаем скорость
            if (Mathf.Abs(speed) < 0.2f)
            {
                speed = 0.0f;
            }
            else
            {
                speed = speed * 0.95f;
            }
        }
        // тяга вперед
        else if (vertical > 0)
        {
            // ограничиваем скорость тягой
            // если движется в направлении тяги, то полную скорость набирает за две секунды
            // иначе тормозим за пол секунды
            if (speed >= currentEnginePower)
            {
                speed = currentEnginePower;
            }
            else if (speed > 0)
            {
                speed += currentEnginePower * 0.5f * Time.deltaTime * vertical;
            }
            else
            {
                speed += currentEnginePower * 2.0f * Time.deltaTime * vertical;
            }
        }
        // тяга назад
        else
        {
            // ограничиваем скорость половиной тяги
            // если движется в направлении тяги, то заднюю скорость набирает за две секунды
            // иначе тормозим за пол секунды
            if (speed <= -currentEnginePower * 0.5f)
            {
                speed = -currentEnginePower * 0.5f;
            }
            else if (speed <= 0)
            {
                speed -= currentEnginePower * Time.deltaTime * Mathf.Abs(vertical);
            }
            else
            {
                speed -= currentEnginePower * 2.0f * Time.deltaTime * Mathf.Abs(vertical);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            hitpoints--;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitpoints--;
        }
        else if (collision.gameObject.CompareTag("Fuel"))
        {
            currentEnginePower = enginePower * 1.5f;
            currentRotationSpeed = rotationSpeed * 2.0f;
            engineBustTimer += 30.0f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Amo"))
        {
            rocketsCount += 5;
            hitpoints = hitpointsMax;
            amo = amoMax;
            Destroy(collision.gameObject);
        }
    }
}
