using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buster : MonoBehaviour
{
    [SerializeField] protected float force = 1.0f;
    [SerializeField] protected int lifetime = 20;
    private Rigidbody rb;

    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody>();
        rb.AddForce((Vector3.up + transform.forward - transform.right * Random.Range(0.0f, 1.0f)).normalized * force, ForceMode.Impulse);

        StartCoroutine(TimeHandler());
    }

    IEnumerator TimeHandler()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    protected abstract void GiveBust(Player contact);

    protected void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            GiveBust(player.GetComponentInChildren<Player>());
            Destroy(gameObject);
        }
    }
}
