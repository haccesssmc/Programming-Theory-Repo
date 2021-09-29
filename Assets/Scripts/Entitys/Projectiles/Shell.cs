using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : Projectile
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
