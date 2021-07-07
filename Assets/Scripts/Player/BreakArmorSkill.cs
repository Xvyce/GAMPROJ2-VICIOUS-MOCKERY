using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakArmorSkill : MonoBehaviour
{

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Animator allyAnimator;
    [SerializeField] private Image fillImage;
    [SerializeField] private float animationDuration = 3.0f;


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


    IEnumerator BreakEnemyArmor()
    {
        allyAnimator.SetBool("doingSkill", true);

        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster)
            {
                if (enemy.revivalCount >= enemy.enemyData.ArmorCount)
                {
                    yield return null;
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

        yield return new WaitForSeconds(animationDuration);

        allyAnimator.SetBool("doingSkill", false);
    }

    public void ActivateEnemyBreakArmor()
    {
        if (lvlDataManager.skillPoints >= 100)
        {
            FindObjectOfType<AudioManager>().Play("Break_Armor_SFX");
            StartCoroutine(BreakEnemyArmor());
            lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }

}
