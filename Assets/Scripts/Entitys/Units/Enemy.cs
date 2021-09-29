using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    [SerializeField] float targetingTime = 2;

    float targetingTimer;
    GameObject target;

    void Awake()
    {
        targetingTimer = targetingTime;
        reloadingTimer = 0;
        acceleration = 0;
        target = null;
    }

    void Update()
    {
        // Gun reloading
        Reload();

        // Check target visibility
        CheckTargetVisibility();

        // Simulate AI input (rotate, acceleration),
        // considering target visibility and reloading status
        AiInput();

        // Calculate speed and move
        SpeedCalculation();
        Move();
    }

    void AiInput()
    {
        if (target == null)
        {
            // Decreas acceleration and select target
            SimulateAccelerationInput(false);
            selectTarget();
        }
        else
        {
            // Increas acceleration, rotate to the target
            SimulateAccelerationInput(true);
            float angle = Rotate();
            
            // and if target inrange - shoot
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (angle < 5 && distance < 40 && reloadingTimer <= 0)
            {
                Shoot();
            }
        }
    }

    void SimulateAccelerationInput(bool press)
    {
        if (acceleration < 1 && press)
        {
            acceleration += Time.deltaTime;
        }
        else if (acceleration > 0 && !press)
        {
            acceleration -= Time.deltaTime;
        }

        acceleration = Mathf.Clamp01(acceleration);
    }

    void CheckTargetVisibility()
    {
        if (target == null) return; 

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
#endif

        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit))
        {
            if(hit.collider.CompareTag("Civil"))
            {
                // If detect any Civil Vehicle
                target = hit.collider.gameObject;
            }
            else if(!hit.collider.CompareTag("Fuel") && !hit.collider.CompareTag("Amo") && !hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Projectile"))
            {
                // If detect the Enemy or other NoName object - clear target
                target = null;
            }
        }
        else
        {
            // If nothing detect - clear target
            target = null;
        }
    }

    void selectTarget()
    {
        // targeting cooldown
        if(targetingTimer > 0)
        {
            targetingTimer -= Time.deltaTime;
            return;
        }

        // select a random target from all civil vehicle
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Civil");
        if(targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
            targetingTimer = targetingTime;
        }
    }

    float Rotate()
    {
        // define the lead for the target
        float targetSpeed = target.GetComponent<Civil>().GetSpeed();
        float projectileSpeed = projectile.GetComponent<Shell>().GetSpeed();
        float advance = Vector3.Distance(target.transform.position, transform.position) * targetSpeed / projectileSpeed;

        // the meeting point of the shell and the target, given their speed
        Vector3 meetingPoint = target.transform.position + target.transform.forward * advance;
        meetingPoint = new Vector3(meetingPoint.x, transform.position.y, meetingPoint.z);

        // calculate direction, considering rotation speed
        Vector3 lookAtDir = (meetingPoint - transform.position).normalized;
        float singleStep = rotationSpeed * Time.deltaTime * 0.01745328627927352441f;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtDir, singleStep, 0.0f);

        // turning to the right direction
        transform.rotation = Quaternion.LookRotation(newDir);

        // return the angle of the remaining turn
        return Vector3.Angle(transform.forward, lookAtDir);
    }


}
