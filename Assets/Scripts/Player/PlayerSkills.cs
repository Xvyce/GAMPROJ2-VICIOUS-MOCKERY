using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public EnemyManager enemyManager;

    public void SlowEnemy()
    {
        // Halves the speed of the enemy
        // 1.0 => 0.5
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            enemy.speed = enemy.speed / 2;
        }
    }

    public void FreezeEnemy()
    {
        // Stops the enemies from walking
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            enemy.isWalking = false;
        }
    }

    public void BreakEnemyArmor()
    {
        // If enemy has armor, reduce it by one
        // If no armor, do nothing
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.armorCount <= 0)
                return;
            else
                enemy.armorCount -= 1;
        }
    }
}
