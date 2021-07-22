using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterSkill : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Enemy _enemy;
    [SerializeField] Transform spawnPoint;

    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private int slimesToSpawn;
    [SerializeField] private float timeBetweenSpawns;

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
        yield return new WaitForSeconds(4f);
        _enemy.isWalking = false;
        StartCoroutine(Cast());
    }

    private IEnumerator Cast()
    {
        //enable casting animation
        yield return new WaitForSeconds(5f);
        //disable casting animation

        // After finished casting, censors words then resumes walking
        //foreach(Enemy enemy in _enemyManager.enemyList)
        //{
        //    //censor text of  current enemies that are spawned in the scene
        //    enemy.StartCensor();
        //}

        StartCoroutine(SpawnSlime());

        /*
         * WaitForSeconds(seconds)
         * check if word is typed
         * If(typed = true) do nothing
         * If(typed = false) start spawning projectiles
         */
    }

    IEnumerator SpawnSlime()
    {
        for (int i = 0; i < slimesToSpawn; i++)
        {
            Instantiate(slimePrefab, spawnPoint.position, Quaternion.Euler(30, 0, 0));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        yield break;
    }
}
