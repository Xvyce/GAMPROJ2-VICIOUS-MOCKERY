using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    public EnemyManager enemyManager;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        SkillPointIndicator();
    }

    void SkillPointIndicator()
    {
        fillImage.fillAmount = DataManager.Instance.skillPoints/100;
    }

    private void SlowEnemy()
    {
        // Halves the speed of the enemy
        // 1.0 => 0.5
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            enemy.speed = enemy.speed / 2;
        }
    }

    private IEnumerator FreezeEnemy()
    {
        // Stops the enemies from walking
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            enemy.isWalking = false;
        }

        yield return new WaitForSeconds(3.0f);

        foreach (Enemy enemy in enemyManager.enemyList)
        {
            enemy.isWalking = true;
        }
    }

    private void BreakEnemyArmor()
    {
        // If enemy has armor, reduce it by one (add 1 death == reduce 1 armor)
        // If no armor, do nothing
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.revivalCount >= enemy.enemyData.ArmorCount)
                return;
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

    public void ActivateSlow()
    {
        if (DataManager.Instance.skillPoints >= 100)
        {
            SlowEnemy();
            DataManager.Instance.skillPoints -= 100;
        }
    }

    public void ActivateFreeze()
    {
        if (DataManager.Instance.skillPoints >= 100)
        {
            StartCoroutine(FreezeEnemy());
            DataManager.Instance.skillPoints -= 100;
        }
    }

    public void ActivateEnemyBreakArmor()
    {
        if (DataManager.Instance.skillPoints >= 100)
        {
            BreakEnemyArmor();
            DataManager.Instance.skillPoints -= 100;
        }
    }
}
