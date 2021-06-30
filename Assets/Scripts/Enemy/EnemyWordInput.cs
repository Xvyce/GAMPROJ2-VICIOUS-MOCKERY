using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWordInput : MonoBehaviour
{
    [SerializeField] private Player _player;
    public EnemyManager enemyManager;

    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            //Only checks input if there are enemies
            if (enemyManager.enemyList.Count > 0)
            {
                foreach (char letter in Input.inputString)
                {
                    enemyManager.TypeLetter(letter);
                }

                if (!Input.anyKey)
                {
                    _player.animator.SetBool("isTyping", false);
                    _player.StopGibberish();
                }
                else
                {
                    _player.animator.SetBool("isTyping", true);
                    _player.StartGibberish();
                }
            }

            //If there's no enemy the player returns to idle
            if (enemyManager.enemyList.Count == 0)
            {
                _player.animator.SetBool("isTyping", false);
                _player.StopGibberish();
            }
        }
    }
}


