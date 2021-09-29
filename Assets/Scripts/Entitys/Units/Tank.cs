using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Vehicle
{
    public float enginePower = 5.0f;
    public float rotationSpeed = 90.0f;

    [SerializeField] protected float reloadingTime = 4.0f;
    [SerializeField] protected GameObject projectile;

    protected float acceleration;
    protected float reloadingTimer;

    protected virtual void Reload()
    {
        if (reloadingTimer > 0.0f)
        {
            reloadingTimer -= Time.deltaTime;
        }
    }

    protected virtual void Shoot()
    {
        Instantiate(projectile, transform.position + transform.forward * 2.0f, transform.rotation);
        reloadingTimer = reloadingTime;
    }

    protected void SpeedCalculation()
    {
        // coasting
        if (Mathf.Approximately(acceleration, 0))
        {
            // stop if the speed is very low 
            // otherwise slow down slowly 
            if (Mathf.Abs(speed) < 0.2f)
            {
                speed = 0.0f;
            }
            else
            {
                speed = speed * 0.95f;
            }
        }
        // acceleration forward
        else if (acceleration > 0)
        {
            // limiting the speed to acceleration 
            // if moving forward, then gains full speed in two seconds 
            // otherwise slow down in half a second 
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
                speed += enginePower * 2 * Time.deltaTime * acceleration;
            }
        }
        // acceleration backward
        else
        {
            // limiting the speed to half the acceleration
            // if moving backward, then gains full speed in two seconds 
            // otherwise slow down in half a second 
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
                speed -= enginePower * 2 * Time.deltaTime * Mathf.Abs(acceleration);
            }
        }
    }
}
