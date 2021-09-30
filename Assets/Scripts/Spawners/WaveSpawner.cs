using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the parrent ZoneSpawner class
public class WaveSpawner : ZoneSpawner
{
    [SerializeField] float cooldownTime = 15;
    [SerializeField] List<GameObject> enemies;

    int stage = 0;
    bool isWaiting = false;


    void Update()
    {
        CheckWave();
    }


    void CheckWave()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine(spawnWave());
        }
    }


    IEnumerator spawnWave()
    {
        yield return new WaitForSeconds(cooldownTime);

        stage++;
        for (int i = 0; i < stage; i++)
        {
            SpawnSingle(enemies);
        }

        isWaiting = false;
    }
}
