using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthbar;
    public float CurrentHealth;
    private float MaxHealth;
    LevelDataManager player;

    private void Start()
    {
        healthbar = GetComponent<Image>();
        player = FindObjectOfType<LevelDataManager>();

        MaxHealth = player.playerMaxHealth;
    }

    private void Update()
    {
        CurrentHealth = player.playerCurrentHealth;
        healthbar.fillAmount = CurrentHealth / MaxHealth;
    }
}
