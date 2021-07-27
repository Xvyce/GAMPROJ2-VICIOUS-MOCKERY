using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    Light candleLight;
    public float minIntensity;
    public float maxIntensity;
    public float timeDelay;
    

    private void Start()
    {
        
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