using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWordInput : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    public static bool hasActiveWord;

    private void Update()
    {
        //if(!hasActiveWord)
        //{
        //    foreach(char letter in Input.inputString)
        //    {
        //    _enemy.TypeLetter(letter);
        //    }
        //}

        foreach (char letter in Input.inputString)
        {
            _enemy.TypeLetter(letter);
        }
    }
}
