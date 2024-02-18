using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnTimer;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SpawnEnemy();
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void SpawnEnemy()
    {
        var pos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            spawnHeight,
            Random.Range(-spawnArea.y, spawnArea.y)
        );

        Instantiate(enemyObject, pos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.up * spawnHeight, new Vector3(spawnArea.x * 2, 0, spawnArea.y * 2));
    }
}
