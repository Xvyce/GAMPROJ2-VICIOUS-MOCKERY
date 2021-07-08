using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputCommence : MonoBehaviour
{
    public TypingUICommence typingUIcommence;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            typingUIcommence.TypeLetter(letter);
        }
    }
}
