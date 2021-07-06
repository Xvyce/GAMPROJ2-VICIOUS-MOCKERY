using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private GameObject waveIndicatorBanner;
    [SerializeField] private TextMeshProUGUI waveIndicatorText;
    [SerializeField] private Transform[] spawnPointsTop;
    [SerializeField] private Transform[] spawnPointsMid;
    [SerializeField] private Transform[] spawnPointsBot;
    [SerializeField] private Transform[] spawnPointsAir;
    [SerializeField] private Transform tutorialSpawnPoint;
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;
    private int nextWave = 0;

    private float enemySearchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        waveIndicatorText.enabled = false;
        waveIndicatorBanner.SetActive(false);
    }

    private void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if(!lvlDataManager.IsGameOver)
            {
                if (!EnemyIsAlive())
                {
                    // If all enemies from last wave is dead, start next wave
                    StartNextWave();
                }
                else
                {
                    return;
                }
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
                StartCoroutine(WaveIndicator());
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
            state = SpawnState.Spawning;
            lvlDataManager.GameOverWin();

            Debug.Log("All waves complete");
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

    IEnumerator WaveIndicator()
    {
        FindObjectOfType<AudioManager>().Play("Wave_Indicator_SFX");
        waveIndicatorText.text = waves[nextWave].name;
        waveIndicatorBanner.SetActive(true);
        waveIndicatorText.enabled = true;

        yield return new WaitForSeconds(1.0f);

        waveIndicatorBanner.SetActive(false);
        waveIndicatorText.enabled = false;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        foreach(WaveContent WC in _wave.waveContent)
        {
            for(int i =0; i < WC.count; i++)
            {
                SpawnEnemy(WC.enemyPrefab);
                yield return new WaitForSeconds(_wave.timeBetweenSpawn);
            }
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int randomSpawnPoint;
        EnemyData type;

        type = _enemy.GetComponent<Enemy>().enemyData;

        if(currentScene == "TutorialTest")
        {
            Instantiate(_enemy, tutorialSpawnPoint.position, Quaternion.Euler(30, 0, 0));
        }
        else
        {
            switch (type.Type)
            {
                case EnemyType.Goblin:
                    randomSpawnPoint = Random.Range(0, spawnPointsMid.Length);
                    Instantiate(_enemy, spawnPointsMid[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                    break;

                case EnemyType.Orc:
                    randomSpawnPoint = Random.Range(0, spawnPointsBot.Length);
                    Instantiate(_enemy, spawnPointsBot[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                    break;

                case EnemyType.Boss:
                    Instantiate(_enemy, spawnPointsMid[1].position, Quaternion.Euler(30, 0, 0));
                    break;

                case EnemyType.Caster:
                    Instantiate(_enemy, spawnPointsMid[1].position, Quaternion.Euler(30, 0, 0));
                    break;

                case EnemyType.Support:
                    randomSpawnPoint = Random.Range(0, spawnPointsAir.Length);
                    Instantiate(_enemy, spawnPointsAir[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                    break;
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public string name;
    public float timeBetweenSpawn;
    public List<WaveContent> waveContent;
}

[System.Serializable]
public class WaveContent
{
    public GameObject enemyPrefab;
    public int count;
}

public enum SpawnState
{
    Spawning,
    Waiting,
    Counting
};
