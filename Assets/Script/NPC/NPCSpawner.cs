using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public bool isSpawn;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject NPCprefabs;
    [SerializeField] private float spawnTime;
    [SerializeField] private float lastSpawnTime;
    [SerializeField] private float currentNPC;
    [SerializeField] private float maxNPC;

    private void Update()
    {
        SpawnTimer();
    }

    private void SpawnTimer()
    {
        if (Time.time > lastSpawnTime + spawnTime && currentNPC < maxNPC)
        {
            GameObject spawnObject = Instantiate<GameObject>(NPCprefabs, spawnPoint.position, Quaternion.identity);
            spawnTime = Time.time;
            currentNPC++;

            isSpawn = true;
        }
    }
}
