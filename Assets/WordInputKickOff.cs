using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInputKickOff : MonoBehaviour
{
    public TypingKickOff typingUIKickOff;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIKickOff.TypeLetter(letter);
        }
    }
}
