using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string[] firstDialogue;
    [SerializeField] private string[] secondDialogue;
    [SerializeField] private string[] thirdDialogue;
    [SerializeField] private string[] fourthDialogue;
    [SerializeField] private string[] fifthDialogue;
    [SerializeField] private float typingSpeed = .02f;
    [SerializeField] private GameObject tutorialInterface;
    private int index;

    private int dialogueIndex=0;
    [SerializeField] TutorialWaveSpawner waveSpawner;

    private bool isActive;
    private bool canPressSpace;

    [Header("UI to Hide")]
    [SerializeField] GameObject slowButton;
    [SerializeField] GameObject scoreCounter;
    [SerializeField] GameObject waveProgressBar;

    private void Start()
    {
        tutorialInterface.SetActive(false);
    }

    private void Update()
    {
        switch(dialogueIndex)
        {
            case 0:
                if (textDisplay.text == firstDialogue[index])
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;

            case 1:
                if (textDisplay.text == secondDialogue[index])
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;

            case 2:
                if (textDisplay.text == thirdDialogue[index])
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;

            case 3:
                if (textDisplay.text == fourthDialogue[index])
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;

            case 4:
                if (textDisplay.text == fifthDialogue[index])
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;
                break;
        }

        if(isActive)
        {
            if(canPressSpace)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Next sentence");
                    NextSentence();
                }
            }
        }
    }

    IEnumerator Type()
    {
        switch(dialogueIndex)
        {
            case 0:
                foreach (char letter in firstDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;

            case 1:
                foreach (char letter in secondDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;

            case 2:
                foreach (char letter in thirdDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;

            case 3:
                foreach (char letter in fourthDialogue[index].ToCharArray())
                {
                    textDisplay.text += letter;
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }

                break;
            case 4:
                foreach (char letter in fifthDialogue[index].ToCharArray())
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

        switch(dialogueIndex)
        {
            case 0:
                if (index < firstDialogue.Length - 1)
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
                    tutorialInterface.SetActive(false);
                    EnableUI();

                    index = 0;
                    waveSpawner.state = TutorialSpawnState.Counting;
                }

                break;

            case 1:
                if (index < secondDialogue.Length - 1)
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
                    tutorialInterface.SetActive(false);
                    EnableUI();

                    waveSpawner.StartNextWave();
                    index = 0;
                    waveSpawner.state = TutorialSpawnState.Counting;
                }

                break;

            case 2:
                if (index < thirdDialogue.Length - 1)
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
                    tutorialInterface.SetActive(false);
                    EnableUI();

                    waveSpawner.StartNextWave();
                    index = 0;
                    waveSpawner.state = TutorialSpawnState.Counting;
                }

                break;

            case 3:
                if (index < fourthDialogue.Length - 1)
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
                    tutorialInterface.SetActive(false);
                    EnableUI();

                    waveSpawner.StartNextWave();
                    index = 0;
                    waveSpawner.state = TutorialSpawnState.Counting;
                }

                break;
            case 4:
                if (index < fifthDialogue.Length - 1)
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
                    tutorialInterface.SetActive(false);
                    EnableUI();

                    waveSpawner.StartNextWave();
                    index = 0;
                    waveSpawner.state = TutorialSpawnState.Counting;
                }

                break;
        }
    }

    public void DisplayUI()
    {
        waveSpawner.state = TutorialSpawnState.Dialogue;
        isActive = true;
        DisableUI();

        tutorialInterface.SetActive(true);
        StartCoroutine(Type());
        Debug.Log("Tutorial Dialogue Manager active");
    }

    void DisableUI()
    {
        if (slowButton != null)
            slowButton.SetActive(false);

        if (waveProgressBar != null)
            waveProgressBar.SetActive(false);

        scoreCounter.SetActive(false);
    }

    void EnableUI()
    {
        if (slowButton != null)
            slowButton.SetActive(true);

        if (waveProgressBar != null)
            waveProgressBar.SetActive(true);

        scoreCounter.SetActive(true);
    }
}
