using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeOutTime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoFadeIn(GetComponent<SpriteRenderer>()));
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    IEnumerator DoFadeIn(SpriteRenderer sprite)
    {
        Color tmp = sprite.color;
        Debug.Log(tmp.a);
        while (tmp.a >0f)
        {
            tmp.a -= Time.deltaTime * fadeOutTime;
            sprite.color = tmp;
            
            

                
           
            //sprite.color = tmp;
        }
        yield return null;
    }
    
}
