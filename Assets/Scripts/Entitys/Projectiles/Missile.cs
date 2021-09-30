using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the Projectile class
public class Missile : Projectile
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float captureAngle;

    private GameObject currentTarget = null;


    void Update()
    {
        selectTarget();

        // POLYMORPHISM
        // call the overrided method
        Move();
    }


    // POLYMORPHISM
    // the Move method from the parrent class has been overrided
    protected override void Move()
    {
        // if the target is selected, then rotate to the target
        if(currentTarget != null)
        {
            // Calculate the actual rotational angle
            Vector3 lookAtDir = (currentTarget.transform.position - transform.position).normalized;
            float singleStep = rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtDir, singleStep, 0.0f);

            // Rotate to the target
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        // call the original method from the parrent class
        // move forward
        base.Move();
    }


    void selectTarget()
    {
        if (currentTarget != null) return;

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
