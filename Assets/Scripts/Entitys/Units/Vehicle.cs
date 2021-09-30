using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// parrent class for other vehicles
public class Vehicle : MonoBehaviour
{
    public float health = 2;

    [SerializeField] protected float damage = 1;
    [SerializeField] protected float speed = 3;


    // POLYMORPHISM
    // created a virtual method
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


    // POLYMORPHISM
    // created a virtual method for overriding in future
    protected virtual void ToDamage(Vehicle contact)
    {
        contact.ChangeHealth(-damage);
    }


    // POLYMORPHISM
    // created a virtual method for overriding in future
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
