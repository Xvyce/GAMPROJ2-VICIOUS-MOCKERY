using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] DialogueManager dialogueManager;

    [Header("Wave Indicator")]
    [SerializeField] private GameObject waveIndicatorBanner;
    [SerializeField] private TextMeshProUGUI waveIndicatorText;
    [SerializeField] private float waveIndicatorTimer = 1.0f;

    [Header("SpawnPoints")]
    [SerializeField] private Transform[] spawnPointsTop;
    [SerializeField] private Transform[] spawnPointsMid;
    [SerializeField] private Transform[] spawnPointsBot;
    [SerializeField] private Transform[] spawnPointsAir;

    [Header("Time Between Wave")]
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;
    public int nextWave = 0;

    private float enemySearchCountdown = 1f;

    [HideInInspector]
    public SpawnState state = SpawnState.Counting;

    [Header("Crystal Settings")]
    [SerializeField] private Transform crystalSpawn;
    [SerializeField] private GameObject crystalPrefab;


    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        waveIndicatorText.enabled = false;
        waveIndicatorBanner.SetActive(false);

        dialogueManager.DisplayStartDialogue();
    }

    private void Update()
    {

        if (state == SpawnState.Waiting)
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

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.Spawning && state != SpawnState.Complete && state != SpawnState.Dialogue)
            {
                StartCoroutine(WaveIndicator(waveIndicatorTimer));
                StartCoroutine(SpawnWave(waves[nextWave]));
                BossWaveCheck();
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

    }

    public void StartNextWave()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.Complete;

            SpawnCrystal();
            dialogueManager.DisplayEndDialogue();

            //lvlDataManager.GameOverWin();

            Debug.Log("All waves complete");
        }
        else
        {
            nextWave++;
        }
    }

    void BossWaveCheck()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (waveIndicatorText.text == "Boss Wave" && currentScene == "Level1")
        {
            FindObjectOfType<AudioManager>().FadeOutTrack("Level_1_BGM");
            FindObjectOfType<AudioManager>().FadeInTrack("Boss_Level_1_BGM", 0.1f);
        }
        else if (waveIndicatorText.text == "Boss Wave" && currentScene == "Level2")
        {
            FindObjectOfType<AudioManager>().FadeOutTrack("Level_2_BGM");
            FindObjectOfType<AudioManager>().FadeInTrack("Boss_Level_2_BGM", 0.2f);
        }
        else if (waveIndicatorText.text == "Boss Wave" && currentScene == "Level3")
        {
            FindObjectOfType<AudioManager>().FadeOutTrack("Level_3_BGM");
            FindObjectOfType<AudioManager>().FadeInTrack("Boss_Level_3_BGM", 0.1f);
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

    IEnumerator WaveIndicator(float timer)
    {
        FindObjectOfType<AudioManager>().Play("Wave_Indicator_SFX");
        waveIndicatorText.text = waves[nextWave].name;
        waveIndicatorBanner.SetActive(true);
        waveIndicatorText.enabled = true;

        yield return new WaitForSeconds(timer);

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
        int randomSpawnPoint;
        EnemyData type;

        type = _enemy.GetComponent<Enemy>().enemyData;

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

            case EnemyType.Armored_Goblin: //added dis
                randomSpawnPoint = Random.Range(0, spawnPointsMid.Length);
                Instantiate(_enemy, spawnPointsMid[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Armored_Orc: //added dis
                randomSpawnPoint = Random.Range(0, spawnPointsBot.Length);
                Instantiate(_enemy, spawnPointsBot[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Boss:
                Instantiate(_enemy, spawnPointsMid[0].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Caster:
                randomSpawnPoint = Random.Range(0, spawnPointsMid.Length);
                Instantiate(_enemy, spawnPointsMid[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Caster_Boss:
                Instantiate(_enemy, spawnPointsMid[0].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Support:
                randomSpawnPoint = Random.Range(0, spawnPointsAir.Length);
                Instantiate(_enemy, spawnPointsAir[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                break;

            case EnemyType.Support_Boss:
                randomSpawnPoint = Random.Range(0, spawnPointsAir.Length);
                Instantiate(_enemy, spawnPointsAir[randomSpawnPoint].position, Quaternion.Euler(30, 0, 0));
                break;
        }
    }

    void SpawnCrystal()
    {
        if(crystalPrefab != null)
        {
            Instantiate(crystalPrefab, crystalSpawn.position, Quaternion.Euler(0, 0, 0));
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
    Counting,
    Dialogue,
    Complete
};
