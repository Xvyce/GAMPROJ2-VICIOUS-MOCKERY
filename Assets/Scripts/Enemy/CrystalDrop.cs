using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDrop : MonoBehaviour
{
    private Enemy _enemy;
    private bool isSpawned;
    [SerializeField] GameObject crystalPrefab;
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if(_enemy.isAlive == false && !isSpawned)
        {
            SpawnCrystal();
            isSpawned = true;
        }
    }

    void SpawnCrystal()
    {
        Instantiate(crystalPrefab, spawnPoint);
    }
}
