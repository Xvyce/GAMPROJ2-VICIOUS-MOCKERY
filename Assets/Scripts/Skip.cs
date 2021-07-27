using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public GameObject transition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skipCutscene1();
        }
    }
    public void skipCutscene1()
    {
        StartCoroutine(LoadLevel1());
    }

    public void skipCutScene2()
    {
        StartCoroutine(MainMenu());
    }

    IEnumerator LoadLevel1()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }
    IEnumerator MainMenu()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
