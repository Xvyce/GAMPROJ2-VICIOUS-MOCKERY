using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private EnemyType type;
    [SerializeField] private int reviveCount;

    public EnemyType Type => type;
    public float Speed => speed;
    public int ReviveCount => reviveCount;
}

public enum EnemyType
{
    Fast,
    Normal,
    Slow,
    Boss
}
