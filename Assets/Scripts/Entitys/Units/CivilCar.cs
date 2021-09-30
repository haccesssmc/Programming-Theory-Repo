using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the vehicle class
public class CivilCar : Vehicle
{
    void Update()
    {
        Move();
        CheckBounds();
    }


    void CheckBounds()
    {
        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }


    public float GetSpeed()
    {
        return speed;
    }
}
