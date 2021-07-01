using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;


    private void Start()
    {
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
        SceneManager.LoadScene(scene.name);
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        GameIsPaused = false;
        Debug.Log("Quitting Game....");
        Application.Quit();
    }
}
