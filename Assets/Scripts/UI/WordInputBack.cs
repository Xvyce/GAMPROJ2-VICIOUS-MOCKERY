using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputBack : MonoBehaviour
{
    public TypingUIBack typingUIback;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            typingUIback.TypeLetter(letter);
        }
    }
}
