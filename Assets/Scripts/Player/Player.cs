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
    public Coroutine gibberishCoroutine;

    string currentScene;
    bool isLose = false;
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

    public void StartSpeakingGibberish()
    {
        gibberishCoroutine = StartCoroutine(SpeakingGibberish());
        keyPressedOnce = true; //added this
        Debug.Log("Speaking Gibberish: True");
    }

    public void StopSpeakingGibberish()
    {
        StopCoroutine(gibberishCoroutine);
        keyPressedOnce = false; //added this
        Debug.Log("Speaking Gibberish: False");
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
    }
}
