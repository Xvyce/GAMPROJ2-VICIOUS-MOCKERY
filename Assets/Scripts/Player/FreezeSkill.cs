using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSkill : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Animator allyAnimator;
    [SerializeField] private Image fillImage;
    [SerializeField] private float freezeDuration = 3.0f;
    [SerializeField] private GameObject particle;

    public bool doingSkill;

    private void Start()
    {
        particle.SetActive(false);
    }

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
        doingSkill = true;
        allyAnimator.SetBool("doingSkill", true);
        // Stops the enemies from walking
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster_Boss && enemy.enemyData.Type != EnemyType.Support_Boss)
            {
                enemy.isWalking = false;
                enemy._animator.speed = 0;
            }
        }

        yield return new WaitForSeconds(freezeDuration);

        doingSkill = false;
        allyAnimator.SetBool("doingSkill", false);
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster_Boss && enemy.enemyData.Type != EnemyType.Support_Boss)
            {
                enemy.isWalking = true;
                enemy._animator.speed = 1.0f;
            }
        }
        // After Freeze Duration enemy will move again
        particle.SetActive(false);
    }

    public void ActivateFreeze()
    {
        if (lvlDataManager.skillPoints >= 100)
        {
            FindObjectOfType<AudioManager>().Play("Freeze_Skill_SFX");
            particle.SetActive(true);
            StartCoroutine(FreezeEnemy());
            lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }

    public void StopFreeze()
    {
        allyAnimator.SetBool("doingSkill", false);
        StopCoroutine(FreezeEnemy());
        particle.SetActive(false);
    }
}
