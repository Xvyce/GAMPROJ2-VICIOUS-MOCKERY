using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] LevelDataManager levelDataManager;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject continueButton;

    [Header("Dialogue Content")]
    [SerializeField] private StartDialogue[] _startDialogue;
    [SerializeField] private EndDialogue[] _endDialogue;

    [SerializeField] private float typingSpeed = .02f;
    [SerializeField] private GameObject dialogueInterface;
    private int index;

    private int dialogueIndex = 0;
    [SerializeField] WaveSpawner waveSpawner;

    private bool isActive;
    private bool canPressSpace;

    private bool isStartDialogue;
    private bool isEndDialogue;

    [Header("UI to disable")]
    [SerializeField] GameObject slowButton;
    [SerializeField] GameObject armorBreakButton;
    [SerializeField] GameObject freezeButton;
    [SerializeField] GameObject scoreCounter;
    [SerializeField] GameObject waveProgressBar;

    [Header("Skill Script")]
    [SerializeField] SlowSkill slowSkill;
    [SerializeField] FreezeSkill freezeSkill;

    [Header("Gibberish")]
    [SerializeField] private Player player;

    //private void Start()
    //{
    //    dialogueInterface.SetActive(false);
    //}

    private void Update()
    {
        switch (dialogueIndex)
        {
            case 0:
                if (textDisplay.text == _startDialogue[index].startDialogue)
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;

            case 1:
                if (textDisplay.text == _endDialogue[index].endDialogue)
                {
                    canPressSpace = true;
                    continueButton.SetActive(true);
                }
                else
                    canPressSpace = false;

                break;
        }

        if (isActive)
        {
            if (canPressSpace)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Next sentence");
                    NextSentence();
                }
            }
        }

        if(isStartDialogue)
        {
            if (_startDialogue[index].startShakeCam == true)
            {
                CameraShaker.Instance.ShakeOnce(0.15f, 0.3f, .1f, 1f);
            }
        }

        if (isEndDialogue)
        {
            if (_endDialogue[index].endShakeCam == true)
            {
                CameraShaker.Instance.ShakeOnce(0.15f, 0.3f, .1f, 1f);
            }
        }
    }

    IEnumerator TypeStartDialogue()
    {
        player.StartSpeakingGibberish();
        characterImage.sprite = _startDialogue[index].startCharacter;
        foreach(char letter in _startDialogue[index].startDialogue.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        player.StopSpeakingGibberish();
    }

    IEnumerator TypeEndDialogue()
    {
        player.StartSpeakingGibberish();
        characterImage.sprite = _endDialogue[index].endCharacter;
        foreach (char letter in _endDialogue[index].endDialogue.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        player.StopSpeakingGibberish();
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        switch (dialogueIndex)
        {
            case 0:
                if (index < _startDialogue.Length - 1)
                {
                    index++;
                    textDisplay.text = "";
                    StartCoroutine(TypeStartDialogue());

                }
                else
                {
                    isActive = false;
                    isStartDialogue = false;
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
                if (index < _endDialogue.Length - 1)
                {
                    index++;
                    textDisplay.text = "";
                    StartCoroutine(TypeEndDialogue());
                }
                else
                {
                    isActive = false;
                    isEndDialogue = false;
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

    public void DisplayStartDialogue()
    {
        isStartDialogue = true;
        waveSpawner.state = SpawnState.Dialogue;
        isActive = true;
        DisableUI();

        dialogueInterface.SetActive(true);
        StartCoroutine(TypeStartDialogue());
        Debug.Log("Start Dialogue Active");
    }

    public void DisplayEndDialogue()
    {
        if(slowSkill != null)
        {
            if(slowSkill.doingSkill)
            {
                slowSkill.StopSlow();
            }
        }

        if(freezeSkill != null)
        {
            if(freezeSkill.doingSkill)
            {
                freezeSkill.StopFreeze();
            }
        }

        isEndDialogue = true;
        waveSpawner.state = SpawnState.Dialogue;
        isActive = true;
        DisableUI();

        dialogueInterface.SetActive(true);
        StartCoroutine(TypeEndDialogue());
        Debug.Log("End Dialogue Active");
    }

    #region ToggleUIFunctions
    void DisableUI()
    {
        if (slowButton != null)
        {
            slowButton.SetActive(false);
        }

        if (armorBreakButton != null)
        {
            armorBreakButton.SetActive(false);
        }

        if (freezeButton != null)
        {
            freezeButton.SetActive(false);
        }

        scoreCounter.SetActive(false);
        waveProgressBar.SetActive(false);
    }

    void EnableUI()
    {
        if (slowButton != null)
        {
            slowButton.SetActive(true);
        }

        if (armorBreakButton != null)
        {
            armorBreakButton.SetActive(true);
        }

        if (freezeButton != null)
        {
            freezeButton.SetActive(true);
        }

        scoreCounter.SetActive(true);
        waveProgressBar.SetActive(true);
    }
    #endregion
}



[System.Serializable]
public class StartDialogue
{
    public Sprite startCharacter;
    public string startDialogue;
    public bool startShakeCam;

}


[System.Serializable]
public class EndDialogue
{
    public Sprite endCharacter;
    public string endDialogue;
    public bool endShakeCam;
}
