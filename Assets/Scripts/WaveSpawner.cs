using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    [SerializeField] private Transform[] spawnPoints;
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
            // for now it returns back to first wave
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
            yield return new WaitForSeconds(1f/_wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        int randomSpawnPoint;
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(_enemy, spawnPoints[randomSpawnPoint].position, Quaternion.Euler(45, 0, 0));
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private string name;
    public Transform enemyPrefab;
    public int count;
    public float spawnRate;
}

public enum SpawnState
{
    Spawning,
    Waiting,
    Counting
};
