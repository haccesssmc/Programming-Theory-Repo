using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float captureAngle;

    private GameObject currentTarget = null;

    void Update()
    {
        if(currentTarget == null)
        {
            selectTarget();
        }

        if(currentTarget != null)
        {
            Move(currentTarget);
        }
        else
        {
            Move();
        }
    }

    void Move(GameObject target)
    {
        // Calculate the actual rotational angle
        Vector3 lookAtDir = (target.transform.position - transform.position).normalized;
        float singleStep = rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtDir, singleStep, 0.0f);

        // Rotate to the target
        transform.rotation = Quaternion.LookRotation(newDir);

        // Move forward
        Move();
    }

    void selectTarget()
    {
        // Get all enemies objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Target selection with minimal deviation (less then the capture angle)
        float minDeviation = captureAngle;
        foreach (GameObject enemy in enemies)
        {
            float enemyDeviation = Vector3.Angle(transform.forward, enemy.transform.position - transform.position);
            if (enemyDeviation < minDeviation)
            {
                currentTarget = enemy;
                minDeviation = enemyDeviation;
            }
        }
    }
}
