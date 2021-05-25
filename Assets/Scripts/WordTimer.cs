using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public WordManager wordManager;
    [SerializeField] private float wordDelayEnemy = 1.5f;

    private float nextWordTimeEnemy = 0f;

    private void Start()
    {
        // Starting in 1 second.
        // a Player Word will spawn every 4.0 seconds
        InvokeRepeating("AddPlayerWord", 1.0f, 4.0f);
    }

    private void Update()
    {
        // Spawns enemies
        if(Time.time >= nextWordTimeEnemy)
        {
            wordManager.AddWord();
            nextWordTimeEnemy= Time.time + wordDelayEnemy;

            wordDelayEnemy *= .99f;
        }
    }

    // Spawns player words (used in start function)
    void AddPlayerWord()
    {
        wordManager.AddPlayerWord();
    }


    // TO DO: Typed player words need to do something (buff self/nerf enemy)
}
