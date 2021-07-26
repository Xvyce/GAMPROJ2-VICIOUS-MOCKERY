using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRearTyping : MonoBehaviour
{
    public TypingRear typingUIRear;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIRear.TypeLetter(letter);
        }
    }
}
