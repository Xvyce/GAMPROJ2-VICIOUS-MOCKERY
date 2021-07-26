using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputPrev : MonoBehaviour
{
    public TypingUiPrev typingUIPrev;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIPrev.TypeLetter(letter);
        }
    }
}
