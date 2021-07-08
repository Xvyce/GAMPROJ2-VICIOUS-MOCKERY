using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSkill : MonoBehaviour
{
    private Enemy _enemy;
    public GameObject screenSplatter;
    public float timebtSplatter = 5f;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        //StartCoroutine(ScreenSplatter());
        InvokeRepeating("StartScreenSplatter", 5.0f, 10.0f);
    }

    private IEnumerator ScreenSplatter()
    {
        yield return new WaitForSeconds(timebtSplatter); // after this
                                             // either instantiate poop or upgrade allies
        Destroy(Instantiate(screenSplatter), 5);
    }

    void StartScreenSplatter()
    {
        StartCoroutine(ScreenSplatter());
    }
}
