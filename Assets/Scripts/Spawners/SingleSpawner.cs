using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawner : Spawner
{
    [SerializeField] float spawnTime = 5;
    [SerializeField] List<GameObject> spawnPrefabs;

    float currentSpawnTime;
    Vector3 spawnZone;

    void Start()
    {
        spawnZone = GetComponent<BoxCollider>().size;
        TimeHandler();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(currentSpawnTime);
 
        spawnSingle(spawnPrefabs, transform.position, spawnZone);
        TimeHandler();
    }

    void TimeHandler()
    {
        currentSpawnTime = spawnTime * Random.Range(0.5f, 1.5f);
        StartCoroutine(Spawn());
    }
}
