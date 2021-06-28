using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
               
                Pause();
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

        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game....");
        Application.Quit();
    }
}
