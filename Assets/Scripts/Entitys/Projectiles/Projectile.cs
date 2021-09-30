using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// parent class for other projectiles
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float lifetime = 3;
    [SerializeField] protected float damage = 1;


    void Awake()
    {
        StartCoroutine(TimeHandler());
    }


    IEnumerator TimeHandler()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }


    // POLYMORPHISM
    // created a virtual method
    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }


    protected virtual void ToDamage(Vehicle target)
    {
        target.ChangeHealth(-damage);
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
