using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TextMeshProUGUI text;

    private float speed;
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    private int revivalCount =0;
    private bool wordTyped;
    private int typoCounter;

    private bool isWalking;

    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    void Start()
    {
        _enemyManager.enemyList.Add(this);
        typoCounter = 0;
        isWalking = true;
        speed = enemyData.Speed;

        GenerateWord();
    }

    void Update()
    {
        if(isWalking)
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
    }

    private void GenerateWord()
    {
        typeIndex = 0;

        switch (enemyData.Type)
        {
            case EnemyType.Slow:
                wordToType = WordGenerator.GetHardWord();
                break;
            case EnemyType.Fast:
                wordToType = WordGenerator.GetEasyWord();
                break;
            case EnemyType.Normal:
                wordToType = WordGenerator.GetNormalWord();
                break;
            case EnemyType.Boss:
                wordToType = WordGenerator.GetBossWord();
                break;
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }

    public char GetNextLetter()
    {
        return wordToType[typeIndex];
    }

    public void TypedWrongLetter()
    {
        typeIndex = 0;
        typoCounter++;
        DataManager.Instance.playerTypo += 1;
        text.text = wordContainer;
        text.color = Color.green;
    }

    public void TypeLetter()
    {
        typeIndex++;
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }

    public bool WordTyped()
    {

        if(typeIndex >= wordToType.Length)
        {
            if (enemyData.Type == EnemyType.Boss && revivalCount < enemyData.ReviveCount)
            {
                wordTyped = false;
                revivalCount++;
                StartCoroutine(BossStagger());
                GetNewWord();
            }
            else
            {
                wordTyped = true;
            }
        }

        if (wordTyped)
        {
            // If word is properly typed without error the score is double
            if (typoCounter == 0)
            {
                DataManager.Instance.playerScore += 2;
            }
            else
            {
                DataManager.Instance.playerScore += 1;
            }

            Destroy(gameObject);
        }

        return wordTyped;
    }

    IEnumerator BossStagger()
    {
        isWalking = false;

        //wait for seconds = boss stagger animation
        yield return new WaitForSeconds(2.0f);

        isWalking = true;
    }

    public void GetNewWord()
    {
        GenerateWord();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Castle")
        {
            DataManager.Instance.Health -= 5f;

            Debug.Log("Enemy have entered the castle");
            _enemyManager.enemyList.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
