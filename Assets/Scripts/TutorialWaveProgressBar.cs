using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialWaveProgressBar : MonoBehaviour
{
    [SerializeField] TutorialWaveSpawner waveSpawner;
    [SerializeField] Image fillImage;

    float currentWave;
    float lastWave;

    // Start is called before the first frame update
    void Start()
    {
        lastWave = waveSpawner.tutorialWaves.Length;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressIndicator();
    }

    void ProgressIndicator()
    {
        currentWave = waveSpawner.nextWave + 1;

        fillImage.fillAmount = currentWave / lastWave;
    }
}
