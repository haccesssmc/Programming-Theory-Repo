using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] [Min(0)] protected float speed;
    [SerializeField] [Min(0)] protected float lifetime;

    protected void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    protected void TimeHandler()
    {
        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }
}
