using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string[] sentences;
    [SerializeField] private float typingSpeed = .02f;
    [SerializeField] private GameObject tutorialInterface;
    private int index;

    [SerializeField] TutorialWaveSpawner waveSpawner;

    private void Start()
    {
        tutorialInterface.SetActive(false);
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            continueButton.SetActive(false);
            tutorialInterface.SetActive(false);

            waveSpawner.state = TutorialSpawnState.Counting;
        }
    }

    public void DisplayUI()
    {
        tutorialInterface.SetActive(true);
        StartCoroutine(Type());
    }
}
