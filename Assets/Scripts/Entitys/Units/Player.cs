using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the Tank class
public class Player : Tank
{
    // ENCAPSULATION
    // protected variables amoMax and healthMax from being changed from other classes
    public int amoMax { get; private set; } = 30;
    public float healthMax { get; private set; } = 5;

    // ENCAPSULATION
    // set custom getters and setters for variables amo, rockets and engineBustTimer
    int m_Amo;
    public int amo
    {
        set
        {
            m_Amo = Mathf.Clamp(value, 0, amoMax);
        }
    }

    int m_Rockets;
    public int rockets 
    {
        get
        {
            return m_Rockets;
        }
        set
        {
            if (value > 0) m_Rockets = value;
        }
    }

    float m_EngineBustTimer;
    public float engineBustTimer
    {
        get
        {
            return m_EngineBustTimer;
        }
        set
        {
            m_EngineBustTimer += value;
            if (m_EngineBustTimer < 0) m_EngineBustTimer = 0;
        }
    }

    [SerializeField] GameObject rocket;

    float rotate;
    float initEnginePower;
    float initRotationSpeed;

    float minX;
    float maxX;
    float minZ;
    float maxZ;


    void Awake()
    {
        Init();
    }


    void Update()
    {
        MovingHandler();
        ShootingHandler();
    }


    void Init()
    {
        PlayerBoundsCalculation();

        initEnginePower = enginePower;
        initRotationSpeed = rotationSpeed;
        m_EngineBustTimer = 0;

        m_Amo = amoMax;
        m_Rockets = 2;
        reloadingTimer = 0;
    }


    void MovingHandler()
    {
        GetMovingInput();
        EngineBustHandler();
        SpeedCalculation();
        Rotate();
        Move();
    }


    void ShootingHandler()
    {
        Reload();

        if (Input.GetKeyDown(KeyCode.Space) && reloadingTimer <= 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
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


    // POLYMORPHISM
    // overrided the base Move method from the parent class
    // to implement a decreasing of the amo count
    protected override void Shoot()
    {
        if(m_Amo > 0)
        {
            m_Amo--;
            base.Shoot();
        }
    }


    void LaunchRocket()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && m_Rockets > 0)
        {
            GameObject obj = Instantiate(rocket, transform.position + transform.forward * 2, transform.rotation);
            m_Rockets--;
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


    // POLYMORPHISM
    // override the base Move method from the parrent class
    protected override void Move()
    {
        Vector3 newPos = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, minX, maxX), newPos.y, Mathf.Clamp(newPos.z, minZ, maxZ));
    }


    void EngineBustHandler()
    {
        if(m_EngineBustTimer > 0)
        {
            m_EngineBustTimer -= Time.deltaTime;
        }

        if (m_EngineBustTimer < 0)
        {
            enginePower = initEnginePower;
            rotationSpeed = initRotationSpeed;
        }
    }
}
