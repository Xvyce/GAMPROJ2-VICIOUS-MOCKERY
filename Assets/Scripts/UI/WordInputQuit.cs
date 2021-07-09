using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputQuit : MonoBehaviour
{
    public TypingUIQuit typingUIback;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIback.TypeLetter(letter);
        }
    }
}
