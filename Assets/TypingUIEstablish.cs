using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class TypingUIEstablish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    private string wordToType;
   
    private string wordContainer;
    private int typeIndex;
    public LevelChanger levelChanger;
    public TMP_Text TextComponent;
    //BlinkingTextCorutina text; //commented out muna kasi may error. Wala siyang references yun commented out - Luis

    bool hasActiveWord;

    private void Start()
    {
        
        TextComponent.fontStyle = FontStyles.Underline;
        wordToType = word.text;
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
            word.color = Color.yellow;
        }

        if (typeIndex >= wordToType.Length)
        {
            typeIndex = 0;
            FindObjectOfType<AudioManager>().Play("Start_Stage_SFX");
            Debug.Log("Open Book");
            word.text = wordContainer;
            //SceneManager.LoadScene("Game");
            SceneHistory.Instance.LoadScene("Level3");

            //levelChanger.FadeToLevel(2);
            levelChanger.FadeToNextLevel();
        }
    }

    
}
