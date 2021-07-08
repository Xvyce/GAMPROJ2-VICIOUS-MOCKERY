using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip[] gibClips;

    string currentScene;
    bool isLose = false;
    bool gibberishActivated = false;
    public bool keyPressedOnce = false;//added this

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
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


    public void StartSpeakingGibberish(bool isSpeaking)
    {
        keyPressedOnce = true;//added this
        gibberishActivated = isSpeaking;
        Debug.Log("Speaking Gibberish: True");
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

        keyPressedOnce = false;//added this
        Debug.Log("Speaking Gibberish: False");
    }
}
