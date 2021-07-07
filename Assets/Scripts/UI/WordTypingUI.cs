using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordTypingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private OpenBook Flip;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    public TMP_Text TextComponent;

    bool hasActiveWord;

    private void Start()
    {
        TextComponent.fontStyle = FontStyles.Underline;
        wordToType = word.text.ToLower();
       // word.text = wordToType.Remove(0, typeIndex);
       // word.text = wordToType.Insert(0, "_");
        wordContainer = wordToType;
        typeIndex = 0;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            typeIndex++;

            word.text = word.text.Remove(0, 1); 
            word.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            word.text = wordContainer;
            word.color = Color.green;
        }

        if (typeIndex >= wordToType.Length)
        { 
            typeIndex = 0;

            Debug.Log("Open Book");
            Flip.openBtn_Click();
            word.text = wordContainer;
        }
    }
}

