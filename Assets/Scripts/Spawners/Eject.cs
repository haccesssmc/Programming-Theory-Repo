using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject : MonoBehaviour
{
    public float force = 1.0f;
    public float timeOfLeave = 30.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce((Vector3.up + transform.forward - transform.right * Random.Range(0.0f, 1.0f)).normalized * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeOfLeave < 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            timeOfLeave -= Time.deltaTime;
        }
    }
}
