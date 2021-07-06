using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator _animator;
    [SerializeField] private TextMeshProUGUI text;
    private EnemyManager _enemyManager;
    private LevelDataManager lvlDataManager;
    private SpriteRenderer thisSprite;
    private Player player;

    // Related to enemy status
    public float speed;
    public int revivalCount;
    public bool isWalking;
    public bool isWalkingRight;

    // Related to enemy word
    private string wordToType;
    private string wordContainer;
    private int typeIndex;
    private bool wordTyped;
    private int typoCounter;


    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        lvlDataManager = GameObject.FindWithTag("LevelDataManager").GetComponent<LevelDataManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        if (isWalking)
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
        if (isWalkingRight)
        {
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

    }


    public char GetNextLetter()
    {
        return wordToType[typeIndex];
    }

    public void TypedWrongLetter()
    {
        typoCounter++;
        lvlDataManager.playerTypo += 1;

        typeIndex = 0;
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

        if (typeIndex >= wordToType.Length)
        {
            if (revivalCount < enemyData.ArmorCount) // Armored Enemies get a new word
            {
                wordTyped = false;

                if (revivalCount == 0)
                {
                    FirstArmorBreakAnimation();
                    lvlDataManager.wordsTyped += 1;
                }
                else if (revivalCount == 1)
                {
                    SecondArmorBreakAnimation();
                    lvlDataManager.wordsTyped += 1;
                }
                revivalCount++;
            }
            else // Enemies without armor gets destroyed
            {
                wordTyped = true;
            }
            if(enemyData.Type == EnemyType.Armored_Orc || enemyData.Type == EnemyType.Armored_Goblin && revivalCount < enemyData.ArmorCount) // added dis
            {
                wordTyped = false;
                if (revivalCount == 0)
                {    
                    lvlDataManager.wordsTyped += 1;
                    GetNewWordStagger();
                }
                else
                    wordTyped = true;
                revivalCount++;
            }

        }
        

        if (wordTyped)
        {
            // Add some score
            if (typoCounter == 0)// No typo == more score || Typo == less score
            {
                lvlDataManager.playerScore += 2;

                if (lvlDataManager.skillPoints < 100)
                    lvlDataManager.skillPoints += 10;
            }
            else
            {
                lvlDataManager.playerScore += 1;

                if (lvlDataManager.skillPoints < 100)
                    lvlDataManager.skillPoints += 5;
            }

            lvlDataManager.wordsTyped += 1;
            lvlDataManager.enemiesKilled += 1;

            Destroy(gameObject);
        }

        return wordTyped;
    }

    // Armor Break Animations
    private IEnumerator StaggerOne()
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss:
                isWalking = false;
                _animator.SetBool("Stagger_One", true);
                FindObjectOfType<AudioManager>().Play("Ogre_Noise_SFX");

                //wait for animation to finish
                yield return new WaitForSeconds(2.1f);

                _animator.SetBool("Stagger_One", false);
                speed = enemyData.Speed * 1.5f;
                isWalking = true;

                _animator.SetBool("Helmet_Walking", true);
                GetNewWord();
                break;

            case EnemyType.Goblin:
                isWalking = false;
                _animator.SetBool("Stagger_One", true);
                //Play goblin armor break audio

                //wait for animation to finish
                yield return new WaitForSeconds(2.1f);

                _animator.SetBool("Stagger_One", false);
                //speed = enemyData.Speed * 1.5f;
                isWalking = true;

                _animator.SetBool("Helmet_Walking", true);
                GetNewWord();
                break;

            case EnemyType.Orc:
                isWalking = false;
                _animator.SetBool("Stagger_One", true);
                //Play orc armor break audio

                //wait for animation to finish
                yield return new WaitForSeconds(2.1f);

                _animator.SetBool("Stagger_One", false);
                //speed = enemyData.Speed * 1.5f;
                isWalking = true;

                _animator.SetBool("Helmet_Walking", true);
                GetNewWord();
                break;
        }
    }

    private IEnumerator StaggerTwo()
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss:
                isWalking = false;
                _animator.SetBool("Helmet_Walking", false);
                _animator.SetBool("Stagger_Two", true);
                FindObjectOfType<AudioManager>().Play("Ogre_Noise_SFX");

                //wait for animation to finish
                yield return new WaitForSeconds(2.1f);

                _animator.SetBool("Stagger_Two", false);
                speed = enemyData.Speed * 2.0f;
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
    public void GetNewWordStagger() //added dis
    {
        generateWordStagger();
    }

    // Caster skill to censor word
    IEnumerator CensorWord(float timer)
    {
        text.text = "**********";

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
        StartCoroutine(StaggerOne());
    }

    public void SecondArmorBreakAnimation()
    {
        StartCoroutine(StaggerTwo());
    }

    // If enemy collides with the castle, decrease player health
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Castle" && enemyData.Type != EnemyType.Support || other.gameObject.tag == "Player" && enemyData.Type != EnemyType.Support)
       {
            lvlDataManager.playerCurrentHealth -= enemyData.AttackDamage;
            Debug.Log("Enemy has entered the castle");
            EnemyManager.hasActiveEnemy = false;
            _enemyManager.enemyList.Remove(this);
            Destroy(this.gameObject);
       }

       // For the support enemy to move back and forth
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

    private void GenerateWord()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        typeIndex = 0;

        if(currentScene == "Tutorial")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordTutorial();
                    break;

                case EnemyType.Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordTutorial();
                    break;
            }
        }

        if(currentScene == "Level1")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;

                case EnemyType.Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;
                case EnemyType.Armored_Goblin: // added dis
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;
                case EnemyType.Armored_Orc: // added dis
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelOne();
                    break;

                case EnemyType.Support:
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;

                case EnemyType.Caster:
                    wordToType = WordGenerator.GetBossWordLevelOne();
                    break;
            }
        }

        if (currentScene == "Level2")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelTwo();
                    break;

                case EnemyType.Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelTwo();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelTwo();
                    break;

                case EnemyType.Support:
                    wordToType = WordGenerator.GetNormalWordLevelTwo();
                    break;

                case EnemyType.Caster:
                    wordToType = WordGenerator.GetBossWordLevelTwo();
                    break;
            }
        }

        if (currentScene == "Level3")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelThree();
                    break;

                case EnemyType.Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Support:
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.Caster:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;
            }
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }

    private void generateWordStagger() //added dis cuz using generateword() will use the spawn sound again
    {
        string currentScene = SceneManager.GetActiveScene().name;
        typeIndex = 0;

        if (currentScene == "Level1")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    //FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX"); add stagger sound here
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;
                case EnemyType.Armored_Orc: // added dis
                    //FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX"); add stagger sound here
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;
            }
        }

        if (currentScene == "Level2")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;
                case EnemyType.Armored_Orc: // added dis
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;
            }
        }

        if (currentScene == "Level3")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;
                case EnemyType.Armored_Orc: // added dis
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;
            }
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }
}
