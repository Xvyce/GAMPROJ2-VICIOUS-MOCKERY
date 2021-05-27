using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private string wordContainer;

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
