using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private float playerMaxHealth = 100;
    [SerializeField] private float playerCurrentHealth;
    private float playerScore;
    private bool isGameOver;

    public delegate void HealthUpdate(float current, float max);
    public static event HealthUpdate OnHealthChanged;

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
        Health = playerMaxHealth;
    }

    private void GameOver()
    {
        IsGameOver = true;
        //Load summary screen or something
    }
}
