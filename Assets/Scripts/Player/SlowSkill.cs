using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlowSkill : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private LevelDataManager lvlDataManager;
    [SerializeField] private Animator allyAnimator;
    [SerializeField] private Image fillImage;
    [SerializeField] private float slowDuration = 3.0f;
    [SerializeField] private GameObject particle;

    public bool doingSkill;

    private void Start()
    {
        particle.SetActive(false);
    }

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
        doingSkill = true;
        allyAnimator.SetBool("doingSkill", true);
        foreach(Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster_Boss && enemy.enemyData.Type != EnemyType.Support_Boss)
            {
                enemy.speed = enemy.speed / 2;
                enemy._animator.speed = .5f;
            }
        }

        yield return new WaitForSeconds(slowDuration);

        doingSkill = false;
        allyAnimator.SetBool("doingSkill", false);
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss && enemy.enemyData.Type != EnemyType.Caster_Boss && enemy.enemyData.Type != EnemyType.Support_Boss)
            {
                enemy.speed = enemy.enemyData.Speed;
                enemy._animator.speed = 1.0f;
            }
        }
        // After Slow Duration enemy returns to its original speed
        particle.SetActive(false);
    }

    public void ActivateSlow()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (lvlDataManager.skillPoints >= 100)
        {
            particle.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Slow_Skill_SFX");
            StartCoroutine(SlowEnemy());

            if(currentScene == "Tutorial")
            {
                Debug.Log("Tutorial scene, no reduction in skill points");
            }
            else
                lvlDataManager.skillPoints -= 100;
        }

        lvlDataManager.skillUseCount += 1;
    }

    public void StopSlow()
    {
        allyAnimator.SetBool("doingSkill", false);
        //StopCoroutine(SlowEnemy());
        particle.SetActive(false);
    }
}
