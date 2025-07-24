using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public event Action OnEnemiesSpawned;

    private EnemyEntity[] enemies;

    private float minX = -0.72f;
    private float maxX = 0.72f;
    private float yPos = 0f;
    private float minZ = -3.6f;
    private float maxZ = 0f;
    private float minDistance = 0.16f;

    public void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        List<Vector3> usedPositions = new List<Vector3>();
        enemies = EnemyPool.Instance.GetAllEnemies();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ForceDeactivate();
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i].gameObject;
            Vector3 spawnPosition;
            int attempts = 0;
            bool validPosition;

            do
            {
                spawnPosition = new Vector3(
                    UnityEngine.Random.Range(minX, maxX),
                    yPos,
                    UnityEngine.Random.Range(minZ, maxZ)
                );

                validPosition = true;
                foreach (Vector3 pos in usedPositions)
                {
                    if (Vector3.Distance(pos, spawnPosition) < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }

                attempts++;

                // на випадок, якщо не можна знайти місце (щоб уникнути вічного циклу)
                if (attempts > 10)
                {
                    Debug.Log("Could not find valid spawn position.");
                    break;
                }

            } while (!validPosition);

            if (validPosition)
            {
                enemy.transform.position = spawnPosition;
                enemy.SetActive(true);
                usedPositions.Add(spawnPosition);

                yield return new WaitForSeconds(0.1f);
            }
        }
        OnEnemiesSpawned?.Invoke();
    }
}