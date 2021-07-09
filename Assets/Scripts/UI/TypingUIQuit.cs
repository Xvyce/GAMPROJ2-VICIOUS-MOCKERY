using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingUIQuit : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI wordBack;
    
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    public TMP_Text TextComponent;

    bool hasActiveWord;

    private void Start()
    {
        TextComponent.fontStyle = FontStyles.Underline;
        wordToType = wordBack.text.ToLower();
        wordContainer = wordToType;
        typeIndex = 0;
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            wordBack.text = wordBack.text.Remove(0, 1);
            wordBack.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            wordBack.text = wordContainer;
            wordBack.color = Color.green;
        }

        if (typeIndex >= wordToType.Length)
        {
            typeIndex = 0;
            wordBack.text = wordContainer;
            Application.Quit();
            Debug.Log("Quit Game");
        }
    }
}
