using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    Light candleLight;
    public float timeDelay;

    private void Start()
    {
        candleLight = GetComponent<Light>();
    }
    private void Update()
    {
        //if()
        //{
        //    StartCoroutine(FlickeringLight());
        //}
        //candleLight.intensity = ;
    }

    IEnumerator FlickeringLight()
    {
       
       
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
      
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        
    }


}
