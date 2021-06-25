using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [Range(0.01f, 0.1f)]
    public float snowCoverSpeed;
    float value = 0;
    private void Start()
    {
        Shader.SetGlobalFloat("_SnowLevel", 0);
    }
    private void Update()
    {
            if (value < 1)
        {
            Shader.SetGlobalFloat("_SnowLevel", value);
            value += Time.deltaTime * snowCoverSpeed;
        }
    }
}
