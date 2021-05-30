using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private int attack;

    [SerializeField] private EnemyType type;
    public EnemyType Type => type;

    public float Speed => speed;
    public GameObject EnemyPrefab => enemyPrefab;
}

public enum EnemyType
{
    Fast,
    Normal,
    Slow,
    Boss
}
