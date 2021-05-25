using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputUI : MonoBehaviour
{
    public WordTypingUI wordTypingUI;

    private void Update()
    {
        foreach(char letter in Input.inputString)
        {
            wordTypingUI.TypeLetter(letter);
        }
    }
}
