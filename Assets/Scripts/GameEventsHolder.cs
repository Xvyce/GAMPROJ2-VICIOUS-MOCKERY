using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventsHolder : MonoBehaviour
{
    public static GameEventsHolder current;

    void Awake()
    {
        current = this;
    }

    public event Action<string> onAudioPlay;
    public void OnAudioCallback(string trackName)
    {
        if (onAudioPlay != null)
        {
            OnAudioCallback(trackName);
        }
    }
}

