using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWord : MonoBehaviour
{
    public TutorialGuide typingUITutorial;

    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typingUITutorial.TypeLetter(letter);
        }
    }
}
