using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingUIBack : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI wordBack;
    [SerializeField] private FlipPage pageFlip;
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
            wordBack.color = Color.yellow;
        }

        if (typeIndex >= wordToType.Length)
        {
            typeIndex = 0;
            wordBack.text = wordContainer;
            Debug.Log("BACK PAGE");
            pageFlip.turnOnePageBtn_Click(FlipPage.ButtonType.BackButton);
        }
    }
}
