using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil : Vehicle
{
    void Update()
    {
        Move();
        if(transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
