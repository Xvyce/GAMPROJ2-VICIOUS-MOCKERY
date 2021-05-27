using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private float speed;

    void Start()
    {
        speed = enemyData.Speed;
    }


    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }
}
