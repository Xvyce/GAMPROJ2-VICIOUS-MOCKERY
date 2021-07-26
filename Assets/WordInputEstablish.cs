using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputEstablish : MonoBehaviour
{
    public TypingUIEstablish typingUIEstablish;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIEstablish.TypeLetter(letter);
        }
    }
}
