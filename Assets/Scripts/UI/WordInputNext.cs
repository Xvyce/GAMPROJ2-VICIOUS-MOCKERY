using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputNext : MonoBehaviour
{
    public TypingUINext typingUInext;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            typingUInext.TypeLetter(letter);
        }
    }
}
