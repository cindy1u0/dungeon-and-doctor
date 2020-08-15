using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // four prefabs for our four different types of enemies
    public GameObject[] enemyPrefabs;
    public int enemyAmount;

    public float xMin = 0;
    public float xMax = 0;
    public float yMin = 0;
    public float yMax = 0;
    public float zMin = 0;
    public float zMax = 0;

    // check if all enemies are spawned - avoid race condition.
    public static bool enemiesSpawned = false;

    void Start()
    {
        enemiesSpawned = false;
        SpawnEnemies();
        enemiesSpawned = true;
    }

    void SpawnEnemies()
    {
        while (enemyAmount > 0)
        {
            SpawnOneEnemy();
            enemyAmount--;
        }
    }

    void SpawnOneEnemy()
    {
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(xMin, xMax);
        enemyPosition.y = Random.Range(yMin, yMax);
        enemyPosition.z = Random.Range(zMin, zMax);

        int randNum = Random.Range(0, enemyPrefabs.Length);
        GameObject spawnEnemy;

        spawnEnemy = Instantiate(enemyPrefabs[randNum], enemyPosition, transform.rotation);

        spawnEnemy.transform.parent = gameObject.transform;
    }
}
