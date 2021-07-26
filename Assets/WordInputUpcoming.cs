using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputUpcoming : MonoBehaviour
{
    public TypingUIUpcoming typingUIUpcoming;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIUpcoming.TypeLetter(letter);
        }
    }
}
