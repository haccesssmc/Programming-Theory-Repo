using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enginePower = 1.5f;
    public float rotationSpeed = 45;
    public float targetingTime = 2.0f;
    public float shootingTime = 4.0f;
    public int hitpointsMax = 2;
    public GameObject projectile;
    
    private float speed = 0.0f;
    private float acceleration = 0.0f;
    private float targetingTimer;
    private float shootingTimer;
    private int hitpoints;
    private GameObject target = null;
    

    // Start is called before the first frame update
    void Start()
    {
        targetingTimer = targetingTime;
        shootingTimer = 0.0f;
        hitpoints = hitpointsMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitpoints <= 0)
        {
            Destroy(gameObject);
        }

        if(target != null)
        {
            checkTargetVisibility();
        }

        if (target == null)
        {
            acceleration = Mathf.Lerp(acceleration, 0.0f, Time.deltaTime * 1.0f);
            if(acceleration > 0.0f)
            {
                acceleration -= Time.deltaTime;
            }
            
            if (acceleration < 0.0f)
            {
                acceleration = 0.0f;
            }

            selectTarget();
        }
        else
        {
            if(acceleration < 1.0f)
            {
                acceleration += Time.deltaTime;
            }

            if (acceleration > 1.0f)
            {
                acceleration = 1.0f;
            }

            float angle = RotateToTarget();

            if(angle < 5.0f && Vector3.Distance(target.transform.position, transform.position) < 40.0f)
            {
                Shoot();
            }
        }

        MoveForward();
    }

    private void checkTargetVisibility()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
        if(Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
        {
            if(hit.collider.CompareTag("Civil"))
            {
                target = hit.collider.gameObject;
            }
            else if(!hit.collider.CompareTag("Fuel") && !hit.collider.CompareTag("Amo") && !hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Projectile"))
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }
    }

    private void selectTarget()
    {
        if(targetingTimer > 0)
        {
            targetingTimer -= Time.deltaTime;
            
            return;
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Civil");
        if(targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
            targetingTimer = targetingTime;
        }
    }

    private float RotateToTarget()
    {
        // определяем упреждение для цели
        float targetSpeed = target.GetComponent<CivilController>().speed;
        float projectileSpeed = projectile.GetComponent<Shell>().GetSpeed();
        float advance = Vector3.Distance(target.transform.position, transform.position) * targetSpeed / projectileSpeed;
        
        // точка встречи снаряда и цели
        Vector3 meetingPoint = target.transform.position + target.transform.forward * advance;
        meetingPoint = new Vector3(meetingPoint.x, transform.position.y, meetingPoint.z);

        // вычисляем направление с учетом скорости разворота
        Vector3 lookAtDir = (meetingPoint - transform.position).normalized;
        float singleStep = rotationSpeed * Time.deltaTime * 0.01745328627927352441f;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtDir, singleStep, 0.0f);

        // поворачиваем к правильному направлению
        transform.rotation = Quaternion.LookRotation(newDir);

        // возвращаем угол оставшегося доворота
        return Vector3.Angle(transform.forward, lookAtDir);
    }

    private void MoveForward()
    {
        // Рассчитываем скороть
        SpeedCalculation();
        // и двигаем вперед с текущей скоростью
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    private void Shoot()
    {
        if(shootingTimer > 0)
        {
            shootingTimer -= Time.deltaTime;
        }
        else
        {
            GameObject obj = Instantiate(projectile, transform.position + transform.forward * 2.0f, transform.rotation);
            shootingTimer = shootingTime;
        }
    }

    private void SpeedCalculation()
    {
        // идет накатом
        if (Mathf.Approximately(acceleration, 0.0f))
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
        else if (acceleration > 0)
        {
            // ограничиваем скорость тягой
            // если движется в направлении тяги, то полную скорость набирает за две секунды
            // иначе тормозим за пол секунды
            if (speed >= enginePower)
            {
                speed = enginePower;
            }
            else if (speed > 0)
            {
                speed += enginePower * 0.5f * Time.deltaTime * acceleration;
            }
            else
            {
                speed += enginePower * 2.0f * Time.deltaTime * acceleration;
            }
        }
        // тяга назад
        else
        {
            // ограничиваем скорость половиной тяги
            // если движется в направлении тяги, то заднюю скорость набирает за две секунды
            // иначе тормозим за пол секунды
            if (speed <= -enginePower * 0.5f)
            {
                speed = -enginePower * 0.5f;
            }
            else if (speed <= 0)
            {
                speed -= enginePower * Time.deltaTime * Mathf.Abs(acceleration);
            }
            else
            {
                speed -= enginePower * 2.0f * Time.deltaTime * Mathf.Abs(acceleration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            hitpoints--;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitpoints--;
        }
    }
}
