using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    protected void spawnSingle(List<GameObject> spawnCollection, Vector3 spawnPoint, Vector3 spawnZone)
    {
        if (spawnCollection.Count < 1) return;

        GameObject spawnObject = spawnCollection[Random.Range(0, spawnCollection.Count)];

        float x = spawnPoint.x + spawnZone.x * Random.Range(-0.5f, 0.5f);
        float y = spawnPoint.y + spawnZone.y * Random.Range(-0.5f, 0.5f);
        float z = spawnPoint.z + spawnZone.z * Random.Range(-0.5f, 0.5f);

        Vector3 spawnPosition = new Vector3(x, y, z);
        Instantiate(spawnObject, spawnPosition, spawnObject.transform.rotation);
    }
}
