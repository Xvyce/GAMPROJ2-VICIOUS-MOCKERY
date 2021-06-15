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
    }

    public void StopGibberish()
    {
        text.enabled = false;
    }
}
