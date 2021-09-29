using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float health { protected get; set; } = 2;

    [SerializeField] protected float damage = 1;
    [SerializeField] protected float speed = 3;

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void ChangeHealth(float delta)
    {
        health += delta;

        if(health <= 0)
        {
            SetIsDestroed();
        }
    }

    protected virtual void ToDamage(Vehicle contact)
    {
        contact.ChangeHealth(-damage);
    }

    protected virtual void SetIsDestroed()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vehicle contact = collision.gameObject.GetComponentInChildren<Vehicle>();
        if(contact != null)
        {
            ToDamage(contact);
        }
    }
}
