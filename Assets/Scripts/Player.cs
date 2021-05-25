using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerWordPrefab;
    [SerializeField] private Transform playerWordCanvas;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    public WordDisplay SpawnPlayerWord()
    {
        GameObject wordObj = Instantiate(playerWordPrefab, playerWordCanvas.transform.position, Quaternion.Euler(45, 0, 0), playerWordCanvas);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }

}
