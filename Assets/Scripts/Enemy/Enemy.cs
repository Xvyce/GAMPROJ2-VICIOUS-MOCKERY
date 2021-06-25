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
    private SpriteRenderer thisSprite;

    public float speed;
    public int revivalCount;
    public bool isWalking;
    public bool isWalkingRight;

    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    private bool wordTyped;
    private int typoCounter;


    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        thisSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _enemyManager.enemyList.Add(this);
        revivalCount = 0;
        typoCounter = 0;
        isWalking = true;
        isWalkingRight = false;
        speed = enemyData.Speed;

        GenerateWord();
    }


    void Update()
    {
        if(isWalking)
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
        if(isWalkingRight)
        {
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
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
            case EnemyType.Support:
                wordToType = WordGenerator.GetNormalWord(); //added this
                break;
            case EnemyType.Caster:
                wordToType = WordGenerator.GetBossWord(); //added this
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
            if (enemyData.Type == EnemyType.Boss && revivalCount < enemyData.ArmorCount) // Orc Boss
            {
                wordTyped = false;

                if(revivalCount==0)
                {
                    FirstArmorBreakAnimation();
                }
                else if(revivalCount ==1)
                {
                    SecondArmorBreakAnimation();
                }
                revivalCount++;
            }
            else // Enemies w/o Armor
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
                DataManager.Instance.skillPoints += 10;
            }
            else
            {
                DataManager.Instance.playerScore += 1;
                DataManager.Instance.skillPoints += 5;
            }

            Destroy(gameObject);
        }

        return wordTyped;
    }

    // Armor Break Animations
    private IEnumerator BossStaggerOne(float timer)
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss:
                isWalking = false;
                _animator.SetBool("Stagger_One", true);

                yield return new WaitForSeconds(timer);

                _animator.SetBool("Stagger_One", false);
                isWalking = true;

                _animator.SetBool("Helmet_Walking", true);
                GetNewWord();
                break;
        }
    }

    private IEnumerator BossStaggerTwo(float timer)
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss:
                isWalking = false;
                _animator.SetBool("Helmet_Walking", false);
                _animator.SetBool("Stagger_Two", true);

                //wait for seconds = boss stagger animation
                yield return new WaitForSeconds(timer);

                _animator.SetBool("Stagger_Two", false);
                isWalking = true;

                _animator.SetBool("Naked_Walking", true);
                GetNewWord();
                break;
        }
    }

    public void GetNewWord()
    {
        GenerateWord();
    }

    // Caster skill to censor word
    IEnumerator CensorWord(float timer)
    {
        text.text = "!@#$%^&*";

        yield return new WaitForSeconds(timer);

        text.text = wordContainer;
    }

    public void StartCensor()
    {
        StartCoroutine(CensorWord(3.0f));
    }

    // To access animation in PlayerSkills script
    public void FirstArmorBreakAnimation()
    {
        StartCoroutine(BossStaggerOne(2.0f));
    }

    public void SecondArmorBreakAnimation()
    {
        StartCoroutine(BossStaggerTwo(2.0f));
    }

    // If enemy collides with the castle, decrease player health
    private void OnTriggerEnter(Collider other)
    {

       if (other.gameObject.tag == "Castle" && enemyData.Type != EnemyType.Support)
       {
            DataManager.Instance.Health -= enemyData.AttackDamage;
            Debug.Log("Enemy have entered the castle");
            EnemyManager.hasActiveEnemy = false;
            _enemyManager.enemyList.Remove(this);
            Destroy(this.gameObject);
       }
       if (other.gameObject.tag == "Castle" && enemyData.Type == EnemyType.Support) // going left so flip when 
       {
            thisSprite.flipX = true;
            isWalking = false;
            isWalkingRight = true;
       }
       if(other.gameObject.tag == "RightWall" && enemyData.Type == EnemyType.Support)
       {
            thisSprite.flipX = false;
            isWalking = true;
            isWalkingRight = false;
       }
    }
}
