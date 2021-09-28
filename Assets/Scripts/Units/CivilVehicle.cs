using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilController : Vehicle
{
    void Update()
    {
        Move();
    }

    public float GetSpeed()
    {
        return speed;
    }
}
