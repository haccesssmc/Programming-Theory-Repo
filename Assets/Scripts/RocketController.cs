using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 180.0f;
    public float timeOfLeave = 3.0f;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        selectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfLeave < 0.0f)
        {
            Destroy(gameObject);
        }

        timeOfLeave -= Time.deltaTime;

        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        // вычисляем направление с учетом скорости разворота
        Vector3 lookAtDir = (target.transform.position - transform.position).normalized;
        float singleStep = rotationSpeed * Time.deltaTime * 0.01745328627927352441f;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, lookAtDir, singleStep, 0.0f);

        // поворачиваем к правильному направлению
        transform.rotation = Quaternion.LookRotation(newDir);

        // и двигаем вперед с текущей скоростью
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    private void selectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float angle = 180.0f;
        foreach (GameObject enemy in enemies)
        {
            float targetAngle = Vector3.Angle(transform.forward, enemy.transform.position - transform.position);
            if (targetAngle < angle)
            {
                target = enemy;
                angle = targetAngle;
            }
        }
    }
}
