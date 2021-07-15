using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingTextCorutina : MonoBehaviour
{
    Text flashingText;
    string textToBlink;
    public float BlinkTime;

    void Awake()
    {
        flashingText = GetComponent<Text>();
        textToBlink = flashingText.text;
        BlinkTime = 0.5f;
    }
    void OnEnable()
    {

        StartCoroutine(BlinkText());
    }



    IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = textToBlink;
            yield return new WaitForSeconds(BlinkTime);
            flashingText.text = string.Empty;
            yield return new WaitForSeconds(BlinkTime);
        }
    }
}
