using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    public EnemySpawner enemySpawner;

    public Player playerWordSpawner;

    private bool hasActiveWord;
    private Word activeWord;

    public void AddWord()
    {
        if(enemySpawner.CheckIfFastEnemy() == true)
        {
            Word word = new Word(WordGenerator.GetEasyWord(), enemySpawner.SpawnWord());

            words.Add(word);
        }
        else if(enemySpawner.CheckIfNormalEnemy() == true)
        {
            Word word = new Word(WordGenerator.GetNormalWord(), enemySpawner.SpawnWord());

            words.Add(word);
        }
        else if(enemySpawner.CheckIfSlowEnemy() == true)
        {
            Word word = new Word(WordGenerator.GetHardWord(), enemySpawner.SpawnWord());

            words.Add(word);
        }
    }

    public void AddPlayerWord()
    {
        Word word = new Word(WordGenerator.GetPlayerWord(), playerWordSpawner.SpawnPlayerWord());

        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
            else
            {
                activeWord.TypedWrongLetter();
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        // Player finished typing the word, word gets removed from the list
        if (hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }
}
