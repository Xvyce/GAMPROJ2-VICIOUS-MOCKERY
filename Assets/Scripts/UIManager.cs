using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public bool isWin;
    public bool isLose;

    // UIManager.Instance.WinScreen
    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
        isLose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin == true)
        {
            WinScreen.SetActive(true);
        }

        if (isWin == false)
        {
            WinScreen.SetActive(false);
        }

        if (isLose)
        {
            LoseScreen.SetActive(true);
        }
        else
        {
            LoseScreen.SetActive(false);
        }
    }
}
