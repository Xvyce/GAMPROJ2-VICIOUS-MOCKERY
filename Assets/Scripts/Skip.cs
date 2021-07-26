using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void skipCutscene1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void skipCutScene2()
    {

    }
}
