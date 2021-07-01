using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Animator animator;

    string currentScene;
    bool isLose = false;

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
        //StartCoroutine(SpeakingGibberish());
    }

    public void StopGibberish()
    {
        text.enabled = false;
        //StopCoroutine(SpeakingGibberish());
    }

    /*IEnumerator SpeakingGibberish()
    {
        FindObjectOfType<AudioManager>().Play("PageFlip_SFX");
        yield return new WaitForSeconds(0.1f);
    }*/
}
