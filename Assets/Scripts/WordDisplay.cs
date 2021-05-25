using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private EnemyData enemyData;
    private float speed;

    private string wordContainer;

    private void Start()
    {
        if(enemyData != null)
            speed = enemyData.Speed;
        
    }

    private void Update()
    {
        if(enemyData != null)
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    public void SetWord(string word)
    {
        text.text = word;
        wordContainer = word;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }

    public void ResetWord()
    {
        text.text = wordContainer;
        text.color = Color.green;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }
}
