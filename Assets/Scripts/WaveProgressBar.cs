using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveProgressBar : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] Image fillImage;
    private RectTransform _imgRect;
    private RectTransform _edgeRect;

    public float EdgeMargin = 20;



    float currentWave;
    float lastWave;

    // Start is called before the first frame update
    void Start()
    {
        lastWave = waveSpawner.waves.Length;
        _imgRect = GetComponent<RectTransform>();
        _edgeRect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ProgressIndicator();
        _edgeRect.localPosition = new Vector2(fillImage.fillAmount * _imgRect.rect.width - EdgeMargin,
             _edgeRect.localPosition.y);
    }

    void ProgressIndicator()
    {
        currentWave = waveSpawner.nextWave +1;

        fillImage.fillAmount = currentWave / lastWave;
    }

}
