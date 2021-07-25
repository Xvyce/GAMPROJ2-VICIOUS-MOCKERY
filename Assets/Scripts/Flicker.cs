using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    Light candleLight;
    public int minIntensity;
    public int maxIntensity;
    public float timeDelay;
    bool isFlickering;

    private void Start()
    {
        isFlickering = false;
        candleLight = GetComponent<Light>();
    }

    private void Update()
    {
        candleLight.range = PingPong(Time.time * timeDelay, minIntensity, maxIntensity);

           
    }
    float PingPong(float startingValue, float min, float max)
    {
        return Mathf.PingPong(startingValue, max - min) + min;
    }

}