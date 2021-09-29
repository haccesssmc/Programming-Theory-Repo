using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterSpawner : Spawner
{
    public Vector3 busterSpawn = new Vector3(0.0f, 0.5f, -23.0f);
    public Vector3 busterSpawnRange = new Vector3(10.0f, 0.0f, 0.0f);
    public float busterSpawnTime = 40.0f;
    public List<GameObject> busters;

    private float busterSpawnTimer;

    void Start()
    {
        busterSpawnTimer = busterSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
    }

    void Update()
    {
        if(busterSpawnTimer < 0.0f)
        {
            spawnBuster();
            busterSpawnTimer = busterSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        }
        else
        {
            busterSpawnTimer -= Time.deltaTime;
        }
    }

    private void spawnBuster()
    {
        spawnSingle(busters, busterSpawn, busterSpawnRange);
    }
}
