using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip[] gibClips;

    string currentScene;
    bool isLose = false;
    bool gibberishActivated = false;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (text != null)
        {
            text.text = "!@#$%^&*";

            text.enabled = false;
        }
    }

    private void Update()
    {
        if (currentScene == "LoseScene" && !isLose)
        {
            Debug.Log("Lose Scene");
            animator.SetBool("isLose", true);
            isLose = true;
        }
    }

    public void StartGibberish()
    {
        text.enabled = true;
    }

    public void StopGibberish()
    {
        text.enabled = false;
    }

    /*public void StartSpeakingGibberish(bool isSpeaking)
    {
        gibberishActivated = isSpeaking;
        Debug.Log(gibberishActivated);
        if (gibberishActivated == true)
        {
            StartCoroutine(SpeakingGibberish());
            gibberishActivated = false;
        }
    }

    public void StopSpeakingGibberish()
    {
        StopCoroutine(SpeakingGibberish());
    }

    IEnumerator SpeakingGibberish()
    {
        yield return null;
        
        for (int i = 0; i < gibClips.Length; i++)
        {
            audioSource.clip = gibClips[i];
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
    }*/
}
