using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 7, -5);
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerObj.transform.position;
        transform.rotation = playerObj.transform.rotation;
        transform.Rotate(Vector3.right, 30.0f);
        transform.Translate(Vector3.forward * -10.0f + Vector3.up * 2.0f);
    }
}
