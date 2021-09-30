using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the parrent ZoneSpawner class
public class SingleSpawner : ZoneSpawner
{
    [SerializeField] float spawnTime = 5;
    [SerializeField] List<GameObject> spawnPrefabs;


    void Start()
    {
        TimeHandler();
    }


    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
 
        SpawnSingle(spawnPrefabs);
        TimeHandler();
    }


    void TimeHandler()
    {
        StartCoroutine(Spawn(spawnTime * Random.Range(0.5f, 1.5f)));
    }
}
