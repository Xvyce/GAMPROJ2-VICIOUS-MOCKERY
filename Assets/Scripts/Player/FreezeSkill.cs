using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSkill : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Image fillImage;
    [SerializeField] private float freezeDuration = 3.0f;


    void Update()
    {
        SkillPointIndicator();

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateFreeze();
        }
    }

    void SkillPointIndicator()
    {
        fillImage.fillAmount = lvlDataManager.skillPoints / 100;
    }

    private IEnumerator FreezeEnemy()
    {
        // Stops the enemies from walking
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                enemy.isWalking = false;
                enemy._animator.speed = 0;
            }
        }

        yield return new WaitForSeconds(freezeDuration);

        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                enemy.isWalking = true;
                enemy._animator.speed = 1.0f;
            }
        }

        // After Freeze Duration enemy will move again
    }

    public void ActivateFreeze()
    {
        if (lvlDataManager.skillPoints >= 100)
        {
            StartCoroutine(FreezeEnemy());
            lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }

}
