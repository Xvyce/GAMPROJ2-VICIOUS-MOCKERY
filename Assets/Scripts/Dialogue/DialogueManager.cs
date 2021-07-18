using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string[] startDialogue;
    [SerializeField] private string[] endDialogue;
    [SerializeField] private float typingSpeed = .02f;
    [SerializeField] private GameObject dialogueInterface;
    private int index;

    private int dialogueIndex = 0;
    [SerializeField] WaveSpawner waveSpawner;

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
                    dialogueIndex++;
                    textDisplay.text = "";
                    continueButton.SetActive(false);
                    dialogueInterface.SetActive(false);

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
                    dialogueIndex++;
                    textDisplay.text = "";
                    continueButton.SetActive(false);
                    dialogueInterface.SetActive(false);

                    index = 0;
                    waveSpawner.state = SpawnState.Complete;
                }

                break;
        }
    }

    public void DisplayUI()
    {
        waveSpawner.state = SpawnState.Dialogue;

        dialogueInterface.SetActive(true);
        StartCoroutine(Type());

        Debug.Log("Dialogue manager active");
    }
}
