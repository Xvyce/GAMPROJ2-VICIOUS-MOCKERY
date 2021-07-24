using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveProgressBar : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] Image fillImage;

    float currentWave;
    float lastWave;

    // Start is called before the first frame update
    void Start()
    {
        lastWave = waveSpawner.waves.Length;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressIndicator();
    }

    void ProgressIndicator()
    {
        currentWave = waveSpawner.currentWave;

        fillImage.fillAmount = currentWave / lastWave;
    }
}
