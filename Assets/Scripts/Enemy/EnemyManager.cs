using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    public List<Enemy> enemyList;

    public static bool hasActiveEnemy;
    private Enemy activeEnemy;

    public void TypeLetter(char letter)
    {
        if(hasActiveEnemy)
        {
            if(activeEnemy.GetNextLetter() == letter)
            {
                _player.animator.SetBool("isTypingCorrect", true);
                activeEnemy.TypeLetter();
                FindObjectOfType<AudioManager>().Play("Typing_SFX");
            }
            else
            {
                _player.animator.SetBool("isTypingCorrect", false);
                activeEnemy.TypedWrongLetter();
                FindObjectOfType<AudioManager>().Play("Mistake_SFX");
            }
        }
        else
        {
            foreach(Enemy enemy in enemyList)
            {
                if (enemy.GetNextLetter() == letter)
                {
                    activeEnemy = enemy;
                    hasActiveEnemy = true;
                    _player.animator.SetBool("isTypingCorrect", true);
                    enemy.TypeLetter();
                    FindObjectOfType<AudioManager>().Play("Typing_SFX");
                    break;
                }
            }
        }

        if(hasActiveEnemy && activeEnemy.WordTyped())
        {
            hasActiveEnemy = false;
            enemyList.Remove(activeEnemy);
        }
    }
}
