using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float lifetime = 3;
    [SerializeField] protected float damage = 1;

    protected void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    protected virtual void ToDamage(Vehicle target)
    {
        target.ChangeHealth(-damage);
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

    private void OnTriggerEnter(Collider other)
    {
        Vehicle target = other.gameObject.GetComponentInChildren<Vehicle>();
        if(target != null)
        {
            ToDamage(target);
            Destroy(gameObject);
        }
    }
}
