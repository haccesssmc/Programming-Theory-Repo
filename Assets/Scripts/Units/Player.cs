using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank
{
    [SerializeField] int amoMax = 30;
    [SerializeField] GameObject rocket;
    [SerializeField] float healthMax = 5;

    float rotate;
    float initEnginePower;
    float initRotationSpeed;
    float engineBustTimer;
    int amo;
    int rockets;

    float minX;
    float maxX;
    float minZ;
    float maxZ;

    void Awake()
    {
        PlayerBoundsCalculation();

        initEnginePower = enginePower;
        initRotationSpeed = rotationSpeed;
        engineBustTimer = 0;

        amo = amoMax;
        rockets = 2;
        reloadingTimer = 0;
    }

    void Update()
    {
        // Moving
        GetMovingInput();
        EngineBustHandler();
        SpeedCalculation();
        Rotate();
        Move();

        // Shooting
        Reload();
        if(Input.GetKeyDown(KeyCode.Space) && reloadingTimer <= 0)
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            LaunchRocket();
        }
    }

    void PlayerBoundsCalculation()
    {
        BoxCollider c = GameObject.Find("Ground").GetComponent<BoxCollider>();
        Transform cTrans = c.transform;
        minX = cTrans.position.x - c.size.x * 0.5f * cTrans.localScale.x + 2;
        maxX = cTrans.position.x + c.size.x * 0.5f * cTrans.localScale.x - 2;
        minZ = cTrans.position.z - c.size.z * 0.5f * cTrans.localScale.z + 2;
        maxZ = cTrans.position.z + c.size.z * 0.5f * cTrans.localScale.z - 2;
    }

    protected override void Shoot()
    {
        if(amo > 0)
        {
            amo--;
            base.Shoot();
        }
    }

    void LaunchRocket()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && rockets > 0)
        {
            GameObject obj = Instantiate(rocket, transform.position + transform.forward * 2, transform.rotation);
            rockets--;
        }
    }

    void GetMovingInput()
    {
        acceleration = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
    }

    void Rotate()
    {
        // If (after speed calculation) move to the backward - invert rotation
        //if (speed < 0) rotate *= -1;

        transform.Rotate(Vector3.up, rotate * rotationSpeed * Time.deltaTime);
    }

    protected override void Move()
    {
        Vector3 newPos = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, minX, maxX), newPos.y, Mathf.Clamp(newPos.z, minZ, maxZ));
    }

    void EngineBustHandler()
    {
        if(engineBustTimer > 0)
        {
            engineBustTimer -= Time.deltaTime;
        }

        if (engineBustTimer < 0)
        {
            enginePower = initEnginePower;
            rotationSpeed = initRotationSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fuel"))
        {
            if(engineBustTimer <= 0)
            {
                enginePower *= 1.5f;
                rotationSpeed *= 2;
            }
            engineBustTimer += 30;

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Amo"))
        {
            rockets += 5;
            health = healthMax;
            amo = amoMax;

            Destroy(collision.gameObject);
        }
    }
}
