using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] LevelDataManager levelDataManager;

    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string[] startDialogue;
    [SerializeField] private string[] endDialogue;
    [SerializeField] private float typingSpeed = .02f;
    [SerializeField] private GameObject dialogueInterface;
    private int index;

    private int dialogueIndex = 0;
    [SerializeField] WaveSpawner waveSpawner;

    private bool isActive;

    [Header("UI to disable")]
    [SerializeField] GameObject slowButton;
    [SerializeField] GameObject armorBreakButton;
    [SerializeField] GameObject freezeButton;
    [SerializeField] GameObject scoreCounter;
    [SerializeField] GameObject waveProgressBar;

    //private void Start()
    //{
    //    dialogueInterface.SetActive(false);
    //}

    private void Update()
    {
        switch (dialogueIndex)
        {
            case 0:
                if (textDisplay.text == startDialogue[index])
                {
                    continueButton.SetActive(true);
                }

                break;

            case 1:
                if (textDisplay.text == endDialogue[index])
                {
                    continueButton.SetActive(true);
                }

                break;
        }

        if(isActive)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("Next sentence");
                NextSentence();
            }
        }    
    }

    IEnumerator Type()
    {
        switch (dialogueIndex)
        {
            case 0:
                foreach (char letter in startDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;

            case 1:
                foreach (char letter in endDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        switch (dialogueIndex)
        {
            case 0:
                if (index < startDialogue.Length - 1)
                {
                    index++;
                    textDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    isActive = false;
                    dialogueIndex++;
                    textDisplay.text = "";
                    continueButton.SetActive(false);
                    dialogueInterface.SetActive(false);
                    EnableUI();

                    index = 0;
                    waveSpawner.state = SpawnState.Counting;
                }

                break;

            case 1:
                if (index < endDialogue.Length - 1)
                {
                    index++;
                    textDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    isActive = false;
                    dialogueIndex++;
                    textDisplay.text = "";
                    continueButton.SetActive(false);
                    dialogueInterface.SetActive(false);
                    EnableUI();

                    index = 0;
                    waveSpawner.state = SpawnState.Complete;

                    levelDataManager.GameOverWin();
                }

                break;
        }
    }

    public void DisplayUI()
    {
        waveSpawner.state = SpawnState.Dialogue;
        isActive = true;
        DisableUI();

        dialogueInterface.SetActive(true);
        StartCoroutine(Type());
        Debug.Log("Dialogue manager active");
    }

    #region ToggleUIFunctions
    void DisableUI()
    {
        if (slowButton != null)
            slowButton.SetActive(false);

        if (armorBreakButton != null)
            armorBreakButton.SetActive(false);

        if (freezeButton != null)
            freezeButton.SetActive(false);

        scoreCounter.SetActive(false);
        waveProgressBar.SetActive(false);
    }

    void EnableUI()
    {
        if (slowButton != null)
            slowButton.SetActive(true);

        if (armorBreakButton != null)
            armorBreakButton.SetActive(true);

        if (freezeButton != null)
            freezeButton.SetActive(true);

        scoreCounter.SetActive(true);
        waveProgressBar.SetActive(true);
    }
    #endregion
}



[System.Serializable]
public class StartDialogue
{
    Image character;
    string dialogue;
}


[System.Serializable]
public class EndDialogue
{
    Image character;
    string dialogue;
}
