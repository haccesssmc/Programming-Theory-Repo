using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// inherited from the Projectile class
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
