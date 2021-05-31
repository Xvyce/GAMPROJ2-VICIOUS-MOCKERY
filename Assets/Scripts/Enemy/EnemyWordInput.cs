using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWordInput : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            _enemy.TypeLetter(letter);
        }
    }
}
