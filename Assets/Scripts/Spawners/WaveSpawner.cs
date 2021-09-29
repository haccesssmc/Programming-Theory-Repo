using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : Spawner
{
    [SerializeField] float cooldownTime = 15;
    [SerializeField] List<GameObject> enemies;

    int stage = 0;
    bool waveWaiting = false;
    Vector3 spawnZone;

    void Start()
    {
        spawnZone = GetComponent<BoxCollider>().size;
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !waveWaiting)
        {
            waveWaiting = true;
            StartCoroutine(spawnWave());
        }
    }

    IEnumerator spawnWave()
    {
        yield return new WaitForSeconds(cooldownTime);

        stage++;
        for (int i = 0; i < stage; i++)
        {
            spawnSingle(enemies, transform.position, spawnZone);
        }

        waveWaiting = false;
    }
}
