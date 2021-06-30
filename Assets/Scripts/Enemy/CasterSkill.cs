using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterSkill : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Enemy _enemy;

    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(Stop());
    }

    private IEnumerator Stop() // stops the movement of casters
    {
        yield return new WaitForSeconds(1f);
        _enemy.isWalking = false;
        StartCoroutine(Cast());
    }

    private IEnumerator Cast()
    {
        //add animation for casting spell
        yield return new WaitForSeconds(5f);

        // After finished casting, censors words then resumes walking
        foreach(Enemy enemy in _enemyManager.enemyList)
        {
            //censor text of  current enemies that are spawned in the scene
            enemy.StartCensor();
        }
    }
}
