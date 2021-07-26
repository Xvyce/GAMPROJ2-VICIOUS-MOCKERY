using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public GameObject transition;
    public void skipCutscene1()
    {
        StartCoroutine(LoadLevel1());
    }

    public void skipCutScene2()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadLevel1()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }
}
