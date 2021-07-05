using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowSkill : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Image fillImage;
    [SerializeField] private float slowDuration = 3.0f;


    void Update()
    {
        SkillPointIndicator();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSlow();
        }
    }

    void SkillPointIndicator()
    {
        fillImage.fillAmount = lvlDataManager.skillPoints / 100;
    }

    private IEnumerator SlowEnemy()
    {
        foreach(Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                enemy.speed = enemy.speed / 2;
                enemy._animator.speed = .5f;
            }
        }

        yield return new WaitForSeconds(slowDuration);

        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                enemy.speed = enemy.enemyData.Speed;
                enemy._animator.speed = 1.0f;
            }
        }

        // After Slow Duration enemy returns to its original speed
    }

    public void ActivateSlow()
    {
        if (lvlDataManager.skillPoints >= 100)
        {
            FindObjectOfType<AudioManager>().Play("Slow_Skill_SFX");
            StartCoroutine(SlowEnemy());
            lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }
}
