using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelManager : MonoBehaviour
{
    public GameObject nextPart;
    public bool spawnNextArea = false;

    void Start()
    {
    }

    void Update()
    {
        if (spawnNextArea || (EnemySpawner.enemiesSpawned
            && transform.childCount == 0))
        {
            if (nextPart == null)
            {
                FindObjectOfType<LevelManager>().LevelBeat();
            }
            else
            {
                nextPart.gameObject.SetActive(true);
            }
        }
    }
}
