using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTypingFollowing : MonoBehaviour
{
    public TypingFollowing typingUIFollowing;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUIFollowing.TypeLetter(letter);
        }
    }
}
