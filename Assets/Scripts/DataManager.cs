using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : Singleton<DataManager>
{

    // Health
    [SerializeField] private float playerMaxHealth = 100;
    [SerializeField] private float playerCurrentHealth;
    public delegate void HealthUpdate(float current, float max);
    public static event HealthUpdate OnHealthChanged;

    // Player Score
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI typoCount;

    public float playerScore;
    public int playerTypo;

    // Game State
    private bool isGameOver;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public float Health
    {
        get
        {
            if(playerCurrentHealth <=0)
            {
                playerCurrentHealth = 0;
                GameOver();
            }
            return playerCurrentHealth;
        }
        set
        {
            playerCurrentHealth = value;
            OnHealthChanged?.Invoke(playerCurrentHealth, playerMaxHealth);

            if (playerCurrentHealth <= 0)
                playerCurrentHealth = 0;
        }
    }

    private void Start()
    {
        Instantiate();
    }

    private void Update()
    {
        score.text = ("Score: " + playerScore);
        typoCount.text = ("Typo: " + playerTypo);
    }

    void Instantiate()
    {
        Health = playerMaxHealth;
        playerScore = 0;
        playerTypo = 0;
    }

    private void GameOver()
    {
        IsGameOver = true;
        //Load summary screen or something
    }
}
