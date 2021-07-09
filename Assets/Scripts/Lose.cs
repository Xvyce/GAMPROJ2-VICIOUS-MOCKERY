using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class Lose : MonoBehaviour
{

    public GameObject LoseScene;
    public Text YesText;
    public Text NoText;


    // Start is called before the first frame update
    void Start()
    {
        highlightalphabet('Y');
        highlightalphabet2('N');
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            Continue();
        }

        if ((Input.GetKeyDown(KeyCode.N)))
        {
            FindObjectOfType<AudioManager>().Play("Typing_SFX");
            LoadMenu();
        }
    }

    public void Continue()
    {
        SceneHistory.Instance.PreviousScene();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void highlightalphabet(char colalpha)
    {
        StringBuilder strBuilder = new StringBuilder(YesText.text);
        strBuilder.Replace(colalpha.ToString(), "<color=#FF0000>" + colalpha + "</color>");
        YesText.text = strBuilder.ToString();
    }

    public void highlightalphabet2(char colalpha)
    {
        StringBuilder strBuilder = new StringBuilder(NoText.text);
        strBuilder.Replace(colalpha.ToString(), "<color=#FF0000>" + colalpha + "</color>");
        NoText.text = strBuilder.ToString();
    }
}
