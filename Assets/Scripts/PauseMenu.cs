using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public Text wordfield;
    public Text RestartText;
    public Text MenuText;
    public Text QuitText;
    public TextMeshProUGUI MenuWin;
    public TextMeshProUGUI NxtLvl;


    private void Start()
    {
        highlightalphabet('C');
        highlightalphabet2('R');
        highlightalphabet3('M');
        highlightalphabet4('Q');
        highlightalphabet5('M');
        highlightalphabet6('N');
        GameIsPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameIsPaused)
            {
                Continue();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
                
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameIsPaused)
            {
                Restart();
                
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameIsPaused)
            {
                LoadMenu();
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameIsPaused)
            {
                QuitGame();
                
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if (UIManager.Instance.isWin == true)
            {
                LoadMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (UIManager.Instance.isWin == true)
            {
                NextLvl2();
            }
        }


    }

    public void Continue()
    {
        FindObjectOfType<AudioManager>().UnPause("Level_1_BGM");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        FindObjectOfType<AudioManager>().Pause("Level_1_BGM");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        GameIsPaused = false;
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneHistory.Instance.LoadScene(scene.name);
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneHistory.Instance.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        GameIsPaused = false;
        Debug.Log("Quitting Game....");
        Application.Quit();
    }

    public void NextLvl2()
    {
        SceneHistory.Instance.LoadScene("Level2");
    }

    public void highlightalphabet(char colalpha)
    {
        StringBuilder strBuilder = new StringBuilder(wordfield.text);
        strBuilder.Replace(colalpha.ToString(), "<color=#FF0000>" + colalpha + "</color>");
        wordfield.text = strBuilder.ToString();
    }

    public void highlightalphabet2(char colalpha2)
    {
        StringBuilder strBuilder = new StringBuilder(RestartText.text);
        strBuilder.Replace(colalpha2.ToString(), "<color=#FF0000>" + colalpha2 + "</color>");
        RestartText.text = strBuilder.ToString();
    }

    public void highlightalphabet3(char colalpha3)
    {
        StringBuilder strBuilder = new StringBuilder(MenuText.text);
        strBuilder.Replace(colalpha3.ToString(), "<color=#FF0000>" + colalpha3 + "</color>");
        MenuText.text = strBuilder.ToString();
    }

    public void highlightalphabet4(char colalpha4)
    {
        StringBuilder strBuilder = new StringBuilder(QuitText.text);
        strBuilder.Replace(colalpha4.ToString(), "<color=#FF0000>" + colalpha4 + "</color>");
        QuitText.text = strBuilder.ToString();
    }

    public void highlightalphabet5(char colalpha4)
    {
        StringBuilder strBuilder = new StringBuilder(MenuWin.text);
        strBuilder.Replace(colalpha4.ToString(), "<color=#FF0000>" + colalpha4 + "</color>");
        MenuWin.text = strBuilder.ToString();
    }

    public void highlightalphabet6(char colalpha4)
    {
        StringBuilder strBuilder = new StringBuilder(NxtLvl.text);
        strBuilder.Replace(colalpha4.ToString(), "<color=#FF0000>" + colalpha4 + "</color>");
        NxtLvl.text = strBuilder.ToString();
    }
}

