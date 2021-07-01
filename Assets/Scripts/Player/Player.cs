using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Animator animator;

    private void Start()
    {
        text.text = "!@#$%^&*";

        text.enabled = false;
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
