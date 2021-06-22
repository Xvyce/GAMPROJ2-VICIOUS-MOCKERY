using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI text;
    private EnemyManager _enemyManager;

    public float speed;
    public int armorCount;
    public bool isWalking;

    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    private bool wordTyped;
    private int typoCounter;

    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    void Start()
    {
        _enemyManager.enemyList.Add(this);
        armorCount = 0;
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
            case EnemyType.Goblin:
                wordToType = WordGenerator.GetEasyWord();
                break;
            case EnemyType.Orc:
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
            if (enemyData.Type == EnemyType.Boss && armorCount < enemyData.ArmorCount)
            {
                wordTyped = false;

                if(armorCount==0)
                {
                    StartCoroutine(BossStaggerOne(2.0f));
                }
                else if(armorCount ==1)
                {
                    StartCoroutine(BossStaggerTwo(2.0f));
                }
                armorCount++;
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

    IEnumerator BossStaggerOne(float timer)
    {
        isWalking = false;
        _animator.SetBool("Stagger_One", true);

        yield return new WaitForSeconds(timer);

        _animator.SetBool("Stagger_One", false);
        isWalking = true;

        _animator.SetBool("Helmet_Walking", true);
        GetNewWord();
    }

    IEnumerator BossStaggerTwo(float timer)
    {
        isWalking = false;
        _animator.SetBool("Helmet_Walking", false);
        _animator.SetBool("Stagger_Two", true);

        //wait for seconds = boss stagger animation
        yield return new WaitForSeconds(timer);

        _animator.SetBool("Stagger_Two", false);
        isWalking = true;

        _animator.SetBool("Naked_Walking", true);
        GetNewWord();
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

            EnemyManager.hasActiveEnemy = false;
            _enemyManager.enemyList.Remove(this);

            Destroy(this.gameObject);
        }
    }
}
