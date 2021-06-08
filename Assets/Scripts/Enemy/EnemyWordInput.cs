using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWordInput : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            _enemy.TypeLetter(letter);
        }

        if(!Input.anyKey)
        {
            //No inputs being pressed
            _player.StopGibberish();
            _player.animator.SetBool("notTyping", true);
        }
        else
        {
            _player.StartGibberish();
            _player.animator.SetBool("notTyping", false);
        }
    }
}
