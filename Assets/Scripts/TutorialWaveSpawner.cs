using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWaveSpawner : MonoBehaviour
{
    public TutorialWave[] tutorialWaves;

    [SerializeField] private LevelDataManager lvlDataManager;

    [Header("Tutorial Dialogue")]
    [SerializeField] TutorialDialogueManager dialogueManager;

    [Header("Wave Indicator")]
    [SerializeField] private GameObject waveIndicatorBanner;
    [SerializeField] private TextMeshProUGUI waveIndicatorText;
    [SerializeField] private float waveIndicatorTimer = 1.0f;

    [Header("SpawnPoints")]
    [SerializeField] private Transform tutorialSpawnPoint;

    [Header("Time Between Wave")]
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;
    public int nextWave = 0;

    private float enemySearchCountdown = 1f;

    [HideInInspector]
    public TutorialSpawnState state = TutorialSpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        waveIndicatorText.enabled = false;
        waveIndicatorBanner.SetActive(false);

        DisplayTutorial();
    }

    private void Update()
    {
        if (state == TutorialSpawnState.Waiting)
        {
            if (!lvlDataManager.IsGameOver)
            {
                if (!EnemyIsAlive())
                {
                    // If all enemies from last wave is dead, start dialogue then spawn next wave
                    if (nextWave + 1 > tutorialWaves.Length - 1)
                    {
                        state = TutorialSpawnState.Complete;
                        lvlDataManager.GameOverWin();

                        Debug.Log("All waves complete");
                    }
                    else
                    {
                        DisplayTutorial();
                    }
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
            if (state != TutorialSpawnState.Spawning && state != TutorialSpawnState.Complete && state != TutorialSpawnState.Dialogue)
            {
                StartCoroutine(WaveIndicator(waveIndicatorTimer));
                StartCoroutine(SpawnWave(tutorialWaves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    public void StartNextWave()
    {
        state = TutorialSpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > tutorialWaves.Length - 1)
        {
            state = TutorialSpawnState.Complete;
            lvlDataManager.GameOverWin();

            Debug.Log("All waves complete");
        }
        else
        {
            nextWave++;
        }
    }

    void BossWaveCheck()
    {
        if (waveIndicatorText.text == "Boss Wave")
        {
            //Replace with tutorial sfx/bgm

            //FindObjectOfType<AudioManager>().FadeOutTrack("Level_1_BGM");
            //FindObjectOfType<AudioManager>().FadeInTrack("Boss_Level_1_BGM", 0.1f);
        }
    }

    bool EnemyIsAlive()
    {
        enemySearchCountdown -= Time.deltaTime;

        if (enemySearchCountdown <= 0f)
        {
            enemySearchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator WaveIndicator(float timer)
    {
        FindObjectOfType<AudioManager>().Play("Wave_Indicator_SFX");
        waveIndicatorText.text = tutorialWaves[nextWave].name;
        waveIndicatorBanner.SetActive(true);
        waveIndicatorText.enabled = true;

        yield return new WaitForSeconds(timer);

        waveIndicatorBanner.SetActive(false);
        waveIndicatorText.enabled = false;
    }

    IEnumerator SpawnWave(TutorialWave _wave)
    {
        state = TutorialSpawnState.Spawning;

        foreach (TutorialWaveContent TWC in _wave.waveContent)
        {
            for (int i = 0; i < TWC.count; i++)
            {
                SpawnEnemy(TWC.enemyPrefab);
                yield return new WaitForSeconds(_wave.timeBetweenSpawn);
            }
        }

        state = TutorialSpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        EnemyData type;

        type = _enemy.GetComponent<Enemy>().enemyData;

        Instantiate(_enemy, tutorialSpawnPoint.position, Quaternion.Euler(30, 0, 0));
    }

    void DisplayTutorial()
    {
        dialogueManager.DisplayUI();
    }
}

[System.Serializable]
public class TutorialWave
{
    public string name;
    public float timeBetweenSpawn;
    public List<TutorialWaveContent> waveContent;
}

[System.Serializable]
public class TutorialWaveContent
{
    public GameObject enemyPrefab;
    public int count;
}

public enum TutorialSpawnState
{
    Spawning,
    Waiting,
    Counting,
    Dialogue,
    Complete
};
