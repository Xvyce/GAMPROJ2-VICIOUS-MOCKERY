using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelDataManager : MonoBehaviour
{

    [Header("Health Variables")]
    [SerializeField] private float playerMaxHealth = 100;
    public float playerCurrentHealth;

    [Header("Player Score Variables")]
    public int playerScore;
    public int playerTypo;
    public int wordsTyped;
    public int enemiesKilled;
    public int skillUseCount;

    [Header("Player Skill Points")]
    public float skillPoints;

    [Header("Win Screen Text")]
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private TextMeshProUGUI winTypoCountText;
    [SerializeField] private TextMeshProUGUI winWordsTypedCountText;
    [SerializeField] private TextMeshProUGUI winEnemiesKilledCountText;
    [SerializeField] private TextMeshProUGUI winSkillsUseCountText;

    private bool isGameOver;
    string currentScene;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        Instantiate();
    }

    private void Update()
    {
        if (playerCurrentHealth <= 0  && !isGameOver)
            GameOverLose();
    }

    void Instantiate()
    {
        isGameOver = false;

        if(currentScene == "TutorialTest")
        {
            playerMaxHealth = 20;
            skillPoints = 100;
        }
        else
        {
            skillPoints = 0;
        }

        playerCurrentHealth = playerMaxHealth;
        playerScore = 0;
        playerTypo = 0;
        wordsTyped = 0;
        enemiesKilled = 0;
        skillUseCount = 0;
    }

    public void GameOverWin()
    {
        isGameOver = true;

        winScoreText.text = ("Score: " + playerScore);
        winTypoCountText.text = ("Typo: " + playerTypo);
        winWordsTypedCountText.text = ("WordsTyped: " + wordsTyped);
        winEnemiesKilledCountText.text = ("Monster Slained: " + enemiesKilled);
        winSkillsUseCountText.text = ("Skills Used: " + skillUseCount);

        UIManager.Instance.isWin = true;
    }

    public void GameOverLose()
    {
        SceneHistory.Instance.LoadScene("LoseScene");
    }
}
