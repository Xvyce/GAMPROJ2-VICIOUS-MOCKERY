using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioSource audioBGM;
    void Start()
    {
        audioBGM = GetComponent<AudioSource>();
        audioBGM.Play();
    }
}
