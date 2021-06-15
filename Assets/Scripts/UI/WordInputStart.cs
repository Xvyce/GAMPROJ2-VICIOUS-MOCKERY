using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputStart : MonoBehaviour
{
    public TypingUIStart typingUIstart;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            typingUIstart.TypeLetter(letter);
        }
    }
}
 