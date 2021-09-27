using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Vector3 civilSpawn = new Vector3(30.0f, 0.5f, -25.5f);
    public Vector3 civilCarSpawn1 = new Vector3(30.0f, 0.5f, -28.0f);
    public Vector3 civilCarSpawn2 = new Vector3(30.0f, 0.5f, -31.0f);
    public Vector3 civilSpawnRange = new Vector3(0.0f, 0.0f, 0.5f);
    public float civilSpawnTime = 5.0f;
    public float civilCarSpawnTime = 5.0f;
    public List<GameObject> civils;
    public List<GameObject> civilCars;

    public Vector3 EnemySpawn = new Vector3(0.0f, 0.5f, 29.0f);
    public Vector3 enemySpawnRange = new Vector3(10.0f, 0.0f, 2.0f);
    public List<GameObject> enemies;

    public Vector3 busterSpawn = new Vector3(0.0f, 0.5f, -23.0f);
    public Vector3 busterSpawnRange = new Vector3(10.0f, 0.0f, 0.0f);
    public float busterSpawnTime = 40.0f;
    public List<GameObject> busters;

    private float civilSpawnTimer;
    private float civilCarSpawnTimer1;
    private float civilCarSpawnTimer2;
    private float busterSpawnTimer;

    private int stage = 0;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        civilSpawnTimer = civilSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        civilCarSpawnTimer1 = civilCarSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        civilCarSpawnTimer2 = civilCarSpawnTime * (1 + Random.Range(-0.5f, 0.5f));

        busterSpawnTimer = busterSpawnTime * (1 + Random.Range(-0.5f, 0.5f));

        cooldownTimer = 15.0f;
        //spawnSingle(civils, civilSpawn, civilSpawnRange);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCivils();

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if(cooldownTimer < 0.0f)
            {
                stage++;
                spawnWave(stage);
                cooldownTimer = 15.0f;
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

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

    private void spawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            spawnSingle(enemies, EnemySpawn, enemySpawnRange);
        }
    }

    private void spawnCivils()
    {
        // civils
        if(civilSpawnTimer < 0.0f)
        {
            spawnSingle(civils, civilSpawn, civilSpawnRange);
            civilSpawnTimer = civilSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        }
        else
        {
            civilSpawnTimer -= Time.deltaTime;
        }

        // civil cars 1
        if (civilCarSpawnTimer1 < 0.0f)
        {
            spawnSingle(civilCars, civilCarSpawn1, civilSpawnRange);
            civilCarSpawnTimer1 = civilCarSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        }
        else
        {
            civilCarSpawnTimer1 -= Time.deltaTime;
        }

        // civil cars 2
        if (civilCarSpawnTimer2 < 0.0f)
        {
            spawnSingle(civilCars, civilCarSpawn2, civilSpawnRange);
            civilCarSpawnTimer2 = civilCarSpawnTime * (1 + Random.Range(-0.5f, 0.5f));
        }
        else
        {
            civilCarSpawnTimer2 -= Time.deltaTime;
        }
    }

    private void spawnSingle(List<GameObject> spawnCollection, Vector3 spawnPoint, Vector3 spawnRange)
    {
        if (spawnCollection.Count < 1) return;

        GameObject spawnObject = spawnCollection[Random.Range(0, spawnCollection.Count)];
        Vector3 spawnPosition = spawnPoint + spawnRange * Random.Range(-1.0f, 1.0f);
        Instantiate(spawnObject, spawnPosition, spawnObject.transform.rotation);
    }
}
