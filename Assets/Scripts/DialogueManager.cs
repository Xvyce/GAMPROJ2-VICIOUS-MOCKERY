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
    [SerializeField] private WaveSpawner waveIndex;
    [SerializeField] private GameObject tutorialInterface;
    private int index;

    bool isPaused = false;

    private void Start()
    {
        tutorialInterface.SetActive(false);
    }

    private void Update()
    {
        if(waveIndex.nextWave == 1)
        {
            DisplayUI();
        }

        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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
            isPaused = false;
            tutorialInterface.SetActive(false);
        }
    }

    void DisplayUI()
    {
        isPaused = true;
        tutorialInterface.SetActive(true);
        StartCoroutine(Type());
    }
}
