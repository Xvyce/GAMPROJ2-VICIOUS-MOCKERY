using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject WinScreen;

    public bool isWin;

    // UIManager.Instance.WinScreen
    // Start is called before the first frame update
    void Start()
    {
        isWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin == true)
        {
            WinScreen.SetActive(true);
            LeanTween.moveY(WinScreen, 550, 1);
        }

        if (isWin == false)
        {
            WinScreen.SetActive(false);
        }
    }
}
