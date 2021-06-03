using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    [SerializeField] private Transform[] spawnPointsTop;
    [SerializeField] private Transform[] spawnPointsMid;
    [SerializeField] private Transform[] spawnPointsBot;
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;
    private int nextWave = 0;

    private float enemySearchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;

    }

    private void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if(!EnemyIsAlive())
            {
                // If all the enemies in the current wave is dead
                // start the next wave
                StartNextWave();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void StartNextWave()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete, loop back to first wave");
            // Go to Next Level
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        enemySearchCountdown -= Time.deltaTime;

        if (enemySearchCountdown <= 0f)
        {
            enemySearchCountdown = 1f;

            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        for(int i =0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(_wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        int randomSpawnPoint;
        EnemyData type;

        type = _enemy.GetComponent<Enemy>().enemyData;

        //Sets which spawn point the enemy will spawn depending on their type
        switch (type.Type)
        {
            case EnemyType.Fast:
                randomSpawnPoint = Random.Range(0, spawnPointsMid.Length);
                Instantiate(_enemy, spawnPointsMid[randomSpawnPoint].position, Quaternion.Euler(45, 0, 0));
                break;

            case EnemyType.Normal:
                randomSpawnPoint = Random.Range(0, spawnPointsBot.Length);
                Instantiate(_enemy, spawnPointsBot[randomSpawnPoint].position, Quaternion.Euler(45, 0, 0));
                break;

            case EnemyType.Slow:
                randomSpawnPoint = Random.Range(0, spawnPointsTop.Length);
                Instantiate(_enemy, spawnPointsTop[randomSpawnPoint].position, Quaternion.Euler(45, 0, 0));
                break;

            case EnemyType.Boss:
                randomSpawnPoint = Random.Range(0, spawnPointsTop.Length);
                Instantiate(_enemy, spawnPointsTop[randomSpawnPoint].position, Quaternion.Euler(45, 0, 0));
                break;
        }
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private string name;
    public GameObject enemyPrefab;
    public int count;
    public float spawnRate;
}

public enum SpawnState
{
    Spawning,
    Waiting,
    Counting
};
