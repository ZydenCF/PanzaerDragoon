using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float spawnInterval = 5f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || player == null) return;

       
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos.x += Random.Range(-10f, 10f);
        spawnPos.y += Random.Range(-2f, 2f);


        int index = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
    }
}