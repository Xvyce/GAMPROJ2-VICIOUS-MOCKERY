using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCasterSkill : MonoBehaviour
{
    [SerializeField] private float castSkillTimer;
    private EnemyManager enemyManager;
    private Enemy enemy;
    private Animator animator;

    private void Awake()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Stop());
    }

    private IEnumerator Stop() // stops the movement of casters
    {
        yield return new WaitForSeconds(4f);
        enemy.isWalking = false;
        StartCoroutine(Cast());
    }

    private IEnumerator Cast()
    {
        animator.SetBool("isCasting", true);
        yield return new WaitForSeconds(castSkillTimer);

        // After finished casting, censors words then resumes walking
        foreach (Enemy enemy in enemyManager.enemyList)
        {
            if (enemy.enemyData.Type != EnemyType.Boss || enemy.enemyData.Type != EnemyType.Caster_Boss || enemy.enemyData.Type != EnemyType.Support_Boss)
            {
                enemy.StartCensor();
            }
        }

        animator.SetBool("isCasting", false);

        if (!enemy.isWalkingRight)
            enemy.isWalking = true;
    }
}
