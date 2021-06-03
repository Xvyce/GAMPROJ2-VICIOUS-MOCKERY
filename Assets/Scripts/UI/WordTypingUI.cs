using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordTypingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private OpenBook Flip;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;

    bool hasActiveWord;

    private void Start()
    {
        wordToType = word.text.ToLower();
        wordContainer = wordToType;
        typeIndex = 0;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            word.text = word.text.Remove(0, 1);
            word.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            word.text = wordContainer;
            word.color = Color.green;
        }

        if(typeIndex >= wordToType.Length)
        {
            Debug.Log("NEXT PAGE");
            Flip.openBtn_Click();
        }
    }
}
