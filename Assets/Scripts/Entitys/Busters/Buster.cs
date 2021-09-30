using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// parent class for other busters
public abstract class Buster : MonoBehaviour
{
    [SerializeField] protected float force = 8;
    [SerializeField] protected int lifetime = 20;


    void Awake()
    {
        Init();
    }


    void Init()
    {
        GetComponent<Rigidbody>().AddForce((Vector3.up + transform.forward - transform.right * Random.Range(0.0f, 1.0f)).normalized * force, ForceMode.Impulse);
        StartCoroutine(LifeHandler());
    }


    IEnumerator LifeHandler()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }


    // ABSTRACTION
    // declare abstract method
    protected abstract void GiveBust(Player contact);


    protected void OnCollisionEnter(Collision collision)
    {
        GameObject player = GameObject.Find("Player");
        if (collision.gameObject == player)
        {
            GiveBust(player.GetComponentInChildren<Player>());
            Destroy(gameObject);
        }
    }
}
