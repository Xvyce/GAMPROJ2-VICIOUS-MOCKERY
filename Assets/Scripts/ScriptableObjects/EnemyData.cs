using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float attackDamage;
    [SerializeField] private EnemyType type;
    [SerializeField] private int armorCount;

    public EnemyType Type => type;
    public float Speed => speed;
    public float AttackDamage => attackDamage;
    public int ArmorCount => armorCount;
}

public enum EnemyType
{
    Goblin,
    Orc,
    Armored_Goblin, // added dis
    Armored_Orc,// added dis
    Support,
    Support_Boss,//added dis
    Caster,
    Boss
}
