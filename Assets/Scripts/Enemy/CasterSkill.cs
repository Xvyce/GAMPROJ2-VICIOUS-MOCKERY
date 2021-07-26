using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterSkill : MonoBehaviour
{
    private Enemy _enemy;
    private AudioManager audioManager;
    [SerializeField] Transform spawnPoint;

    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private int slimesToSpawn;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] Animator chairAnimator;

    public int projectilesAlive;

    private void Awake()
    {
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
        // If slimes have been spawned player is forced to stop typing
        // the caster boss so he can type the projectiles
        EnemyManager.hasActiveEnemy = false;

        _enemy.typeIndex = 0;
        _enemy.text.text = _enemy.wordContainer;

        for (int i = 0; i < slimesToSpawn; i++)
        {
            chairAnimator.SetBool("isShooting", true);
            Instantiate(slimePrefab, spawnPoint.position, Quaternion.Euler(30, 0, 0));
            projectilesAlive++;

            yield return new WaitForSeconds(.7f);

            chairAnimator.SetBool("isShooting", false);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        yield break;
    }

    public void CasterBossSkill()
    {
        StartCoroutine(SpawnSlime());
    }
}
