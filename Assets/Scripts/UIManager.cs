using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject WinScreen;

    public bool isWin;
    bool alreadyWin;

    // UIManager.Instance.WinScreen
    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyWin)
        {
            ActivateWinScreen();
        }

        DeactivateWinScreen();
    }

    void ActivateWinScreen()
    {
        if(isWin)
        {
            Debug.Log("Congrats, you've won!");
            WinScreen.SetActive(true);
            LeanTween.moveY(WinScreen, 550, 1);
            alreadyWin = true;
            FindObjectOfType<AudioManager>().Stop("Level_1_BGM");
            FindObjectOfType<AudioManager>().Play("Victory_SFX");
            FindObjectOfType<AudioManager>().Play("Victory_BGM");
        }
    }

    void DeactivateWinScreen()
    {
        if (!isWin)
        {
            WinScreen.SetActive(false);
            alreadyWin = false;
        }
    }
}
