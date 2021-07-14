using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthbar;
    public float CurrentHealth;
    private float MaxHeath = 100f;
    LevelDataManager player;

    private void Start()
    {
        healthbar = GetComponent<Image>();
        player = FindObjectOfType<LevelDataManager>();
    }

    private void Update()
    {
        CurrentHealth = player.playerCurrentHealth;
        healthbar.fillAmount = CurrentHealth / MaxHeath;
    }
}
