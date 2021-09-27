using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : Projectile
{
    void Update()
    {
        TimeHandler();
        Move();
    }

    public float GetSpeed()
    {
        return speed;
    }
}
