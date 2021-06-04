using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private string wordToType;
    private string wordContainer;
    private int typeIndex;

    private void Start()
    {
        currentHealth = maxHealth;

        typeIndex = 0;
        wordToType = WordGenerator.GetPlayerWord();

        if(text != null)
        {
            text.text = wordToType;
        }    
    }

    public void TypeLetter(char letter)
    {
        if (wordToType[typeIndex] == letter)
        {
            typeIndex++;

            text.text = text.text.Remove(0, 1);
            text.color = Color.red;
        }
        else
        {
            typeIndex = 0;

            text.text = wordContainer;
            text.color = Color.green;
        }

        // If word have been typed correctly
        if (typeIndex >= wordToType.Length)
        {
            //Do something
        }
    }
}
