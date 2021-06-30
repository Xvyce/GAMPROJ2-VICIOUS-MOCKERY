using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakArmorSkill : MonoBehaviour
{

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Image fillImage;


    void Update()
    {
        SkillPointIndicator();

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateEnemyBreakArmor();
        }
    }

    void SkillPointIndicator()
    {
        fillImage.fillAmount = lvlDataManager.skillPoints / 100;
    }

    void BreakEnemyArmor()
    {
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                if (enemy.revivalCount >= enemy.enemyData.ArmorCount)
                {
                    return;
                }
                else
                {
                    // Armor Break animations
                    if (enemy.revivalCount == 0)
                    {
                        enemy.FirstArmorBreakAnimation();
                    }
                    else if (enemy.revivalCount == 1)
                    {
                        enemy.SecondArmorBreakAnimation();
                    }

                    enemy.revivalCount += 1;
                }
            }
        }
    }

    public void ActivateEnemyBreakArmor()
    {
        if (lvlDataManager.skillPoints >= 100)
        {
            BreakEnemyArmor();
            lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }

}
