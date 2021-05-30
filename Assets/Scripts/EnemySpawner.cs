using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData enemyData;

    private GameObject enemyPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        enemyPrefab = enemyData.EnemyPrefab;
    }

    public WordDisplay SpawnWord()
    {
        int randomSpawnPoint;
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject wordObj = Instantiate(enemyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.Euler(45,0,0), parent);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }

    public bool CheckIfFastEnemy()
    {
        bool isFast = false;

        if (enemyData.Type == EnemyType.Fast)
            isFast = true;
        else
            isFast = false;

        return isFast;
    }

    public bool CheckIfNormalEnemy()
    {
        bool isNormal = false;

        if (enemyData.Type == EnemyType.Normal)
            isNormal = true;
        else
            isNormal = false;

        return isNormal;
    }

    public bool CheckIfSlowEnemy()
    {
        bool isSlow = false;

        if (enemyData.Type == EnemyType.Slow)
            isSlow = true;
        else
            isSlow = false;

        return isSlow;
    }
}
