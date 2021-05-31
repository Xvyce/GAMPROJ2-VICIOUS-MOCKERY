using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TextMeshProUGUI text;

    private float speed;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    private int revivalCount =0;

    void Start()
    {
        speed = enemyData.Speed;
        typeIndex = 0;

        GenerateWord();
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    public void GenerateWord()
    {
        switch (enemyData.Type)
        {
            case EnemyType.Slow:
                wordToType = WordGenerator.GetHardWord();
                break;
            case EnemyType.Fast:
                wordToType = WordGenerator.GetEasyWord();
                break;
            case EnemyType.Normal:
                wordToType = WordGenerator.GetNormalWord();
                break;
            case EnemyType.Boss:
                wordToType = WordGenerator.GetBossWord();
                break;
        }
        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            text.text = text.text.Remove(0, 1);
            text.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            text.text = wordContainer;
            text.color = Color.green;
        }

        // If word have been typed correctly
        if (typeIndex >= wordToType.Length)
        {
            if (enemyData.Type == EnemyType.Boss && revivalCount < enemyData.ReviveCount)
            {
                revivalCount++;
                GetNewWord();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void GetNewWord()
    {
        typeIndex = 0;
        GenerateWord();
    }

    //TO DO: Should only have 1 active word at all times
}
