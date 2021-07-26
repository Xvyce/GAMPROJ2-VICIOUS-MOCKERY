using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioEvent : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    void onEnable()
    {
        GameEventsHolder.current.onAudioPlay += HandleAudioPlay;
    }

    void onDisable()
    {
        GameEventsHolder.current.onAudioPlay -= HandleAudioPlay;
    }

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void HandleAudioPlay(string toPlay)
    {
        audioManager.Play(toPlay);
    }
}
