using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    string currentScene;
    bool keepFadingIn, keepFadingOut;


    protected virtual void Awake()
    {
        //GameObject doesn't persist (even when IsPersist = true) when script is inheriting singleton script
        //so script has its own singleton

        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name; 

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    public void Start()
    {
        if (currentScene == "MainMenu")
        {
            FadeInTrack("Main_Menu_BGM", 0.1f);
            FadeInTrack("Tavern_Ambience_SFX", 0.2f);
        }
        else if (currentScene == "Tutorial")
        {
            Play("Tutorial_BGM");
            Play("Tavern_Ambience_SFX");
        }
        else if (currentScene == "Level1")
        {
            FadeInTrack("Level_1_BGM", 0.1f);
        }
        else if (currentScene == "Level2")
        {
            FadeInTrack("Level_2_BGM", 0.4f);
        }
        else if (currentScene == "Level3")
        {
            FadeInTrack("Level_3_BGM", 0.1f);
        }
        else if (currentScene == "LoseScene")
        {
            Play("Lose_BGM");
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }


        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.UnPause();
    }

    #region FadeIn/FadeOut
    public void FadeInTrack(string name, float maxVolume)
    {
        Play(name);
        StartCoroutine(FadeIn(name, maxVolume));
    }

    public void FadeOutTrack(string name)
    {
        StartCoroutine(FadeOut(name));
    }

    IEnumerator FadeIn(string name, float maxVolume)
    {
        float timeToFade = 2.7f;
        float timeElapsed = 0f;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.volume = 0;

        while(timeElapsed < timeToFade)
        {
            s.source.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(string name)
    {
        float timeToFade = 2.7f;
        float timeElapsed = 0f;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        float audioVolume = s.source.volume;

        while(timeElapsed < timeToFade)
        {   
            s.source.volume = Mathf.Lerp(audioVolume, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            Debug.Log(s.source.volume);
            yield return null;
        }
    }
    #endregion
}


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

