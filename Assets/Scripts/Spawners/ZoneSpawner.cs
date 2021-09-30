using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// parent class for other zone spawners
public class ZoneSpawner : MonoBehaviour
{
    protected Vector3 spawnZone;


    void Awake()
    {
        Init();
    }


    void Init()
    {
        spawnZone = GetComponent<BoxCollider>().size;
    }


    protected void SpawnSingle(List<GameObject> spawnCollection)
    {
        if (spawnCollection.Count < 1) return;

        GameObject spawnObject = spawnCollection[Random.Range(0, spawnCollection.Count)];
        Vector3 spawnPoint = transform.position;

        float x = spawnPoint.x + spawnZone.x * Random.Range(-0.5f, 0.5f);
        float y = spawnPoint.y + spawnZone.y * Random.Range(-0.5f, 0.5f);
        float z = spawnPoint.z + spawnZone.z * Random.Range(-0.5f, 0.5f);

        Vector3 spawnPosition = new Vector3(x, y, z);
        Instantiate(spawnObject, spawnPosition, spawnObject.transform.rotation);
    }
}
