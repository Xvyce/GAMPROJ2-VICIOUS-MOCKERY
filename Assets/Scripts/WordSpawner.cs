using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private Transform wordCanvas;
    [SerializeField] private Transform[] spawnPoints;


    public WordDisplay SpawnWord()
    {
        int randomSpawnPoint;

        randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject wordObj = Instantiate(wordPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity, wordCanvas);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
