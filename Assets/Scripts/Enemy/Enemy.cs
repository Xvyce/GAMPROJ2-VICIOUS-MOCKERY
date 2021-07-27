using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator _animator;
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] private Image typeBox;
    [SerializeField] private Image oneArmorTypeBox;
    [SerializeField] private Image twoArmorTypeBox;
    private EnemyManager _enemyManager;
    private LevelDataManager lvlDataManager;
    private SpriteRenderer thisSprite;
    private Player player;

    // Related to enemy status
    public float speed;
    public int revivalCount;
    public bool isWalking;
    public bool isWalkingRight;
    private bool isDefeat;

    // Related to enemy word
    [HideInInspector] public Color originalColor;
    private string wordToType;
    [HideInInspector] public string wordContainer;
    [HideInInspector] public int typeIndex;
    private bool wordTyped;
    private int typoCounter;
    public bool isAlive;

    // For caster boss
    [SerializeField] CasterSkill casterBoss;

    private void Awake()
    {
        _enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        lvlDataManager = GameObject.FindWithTag("LevelDataManager").GetComponent<LevelDataManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        thisSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        isAlive = true;
        _enemyManager.enemyList.Add(this);
        revivalCount = 0;
        typoCounter = 0;
        originalColor = text.color;
        isWalking = true;
        isWalkingRight = false;
        speed = enemyData.Speed;

        TypeBox();
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
        text.color = originalColor;
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
                EnemyManager.hasActiveEnemy = false;

                if(player.speakingGib)
                {
                    player.StopSpeakingGibberish();
                }

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

        }
        

        if (wordTyped)
        {
            // Add some score
            AddScore();

            //Caster doesnt have run animation
            if(enemyData.Type != EnemyType.CasterProjectile)
            {
                Defeat();
            }
            else //caster boss and slime projectile
            {
                isAlive = false;
                player.StopSpeakingGibberish();
                text.enabled = false;

                if(enemyData.Type == EnemyType.CasterProjectile)
                {
                    if(casterBoss!= null)
                    {
                        casterBoss.projectilesAlive -= 1;
                    }
                    
                    FindObjectOfType<AudioManager>().Play("Slime_Burst_SFX");
                    Destroy(gameObject);
                }
            }
        }

        return wordTyped;
    }

    private void AddScore()
    {
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
    }

    //Defeat Animation
    private void Defeat()
    {
        isAlive = false;
        player.StopSpeakingGibberish();
        isDefeat = true;
        isWalking = false;
        text.enabled = false;
        typeBox.enabled = false;
        speed = speed * 2;

        isWalkingRight = true;
        _animator.SetBool("Is_Defeat", true);
    }

    #region Stagger
    private IEnumerator StaggerOne()
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss: //Armored Ogre Boss
                isWalking = false;
                twoArmorTypeBox.enabled = false;
                _animator.SetBool("Stagger_One", true);

                yield return new WaitForSeconds(2.1f);//wait for animation to end

                _animator.SetBool("Stagger_One", false);
                speed = enemyData.Speed * 1.5f;
                isWalking = true;

                _animator.SetBool("Helmet_Walking", true);
                generateWordStagger();
                break;

            case EnemyType.Armored_Goblin: //Armored Goblin
                oneArmorTypeBox.enabled = false;
                _animator.SetBool("Stagger_One", true);
                //Play goblin armor break audio
                generateWordStagger();

                yield return new WaitForSeconds(0.9f);//wait for animation to end

                _animator.SetBool("Stagger_One", false);
                _animator.SetBool("Naked_Walking", true);

                break;

            case EnemyType.Armored_Orc: //Armored Orc
                oneArmorTypeBox.enabled = false;
                _animator.SetBool("Stagger_One", true);
                //Play orc armor break audio
                generateWordStagger();

                yield return new WaitForSeconds(.92f);//wait for animation to end

                _animator.SetBool("Stagger_One", false);
                _animator.SetBool("Naked_Walking", true);

                break;

            case EnemyType.Support_Boss: //Support Boss
                twoArmorTypeBox.enabled = false;
                _animator.SetBool("Stagger_One", true);
                //Play Support Boss armor break audio

                yield return new WaitForSeconds(1.0f);//wait for animation to end

                _animator.SetBool("Stagger_One", false);
                _animator.SetBool("Helmet_Walking", true);
                generateWordStagger();

                break;

            case EnemyType.Caster_Boss: //Caster Boss
                twoArmorTypeBox.enabled = false;
                _animator.SetBool("Stagger_One", true);
                FindObjectOfType<AudioManager>().Play("Armor_Break_SFX");
                generateWordStagger();
                yield return new WaitForSeconds(1.6f);//wait for animation to end

                _animator.SetBool("Stagger_One", false);
                _animator.SetBool("NoHoodIdle", true);

                casterBoss.CasterBossSkill();
                break;
        }
    }

    private IEnumerator StaggerTwo()
    {
        switch (enemyData.Type)
        {
            case EnemyType.Boss:
                isWalking = false;
                oneArmorTypeBox.enabled = false;
                _animator.SetBool("Helmet_Walking", false);
                _animator.SetBool("Stagger_Two", true);

                yield return new WaitForSeconds(2.1f);//wait for animation to end

                _animator.SetBool("Stagger_Two", false);
                speed = enemyData.Speed * 2.0f;
                isWalking = true;

                _animator.SetBool("Naked_Walking", true);
                generateWordStagger();
                break;

            case EnemyType.Support_Boss:
                oneArmorTypeBox.enabled = false;
                _animator.SetBool("Helmet_Walking", false);
                _animator.SetBool("Stagger_Two", true);
                //Play Support Boss break audio

                yield return new WaitForSeconds(1.0f);//wait for animation to end

                _animator.SetBool("Stagger_Two", false);
                _animator.SetBool("Naked_Walking", true);
                generateWordStagger();


                break;

            case EnemyType.Caster_Boss: //Caster Boss
                oneArmorTypeBox.enabled = false;
                _animator.SetBool("NoHoodIdle", false);
                _animator.SetBool("Stagger_Two", true);
                FindObjectOfType<AudioManager>().Play("Armor_Break_SFX");
                generateWordStagger();
                yield return new WaitForSeconds(1.2f);//wait for animation to end

                _animator.SetBool("Stagger_Two", false);
                _animator.SetBool("NakedIdle", true);

                casterBoss.CasterBossSkill();
                break;
        }
    }
    #endregion

    // Caster skill to censor word
    IEnumerator CensorWord(float timer)
    {
        text.text = "**********";

        yield return new WaitForSeconds(timer);

        text.text = wordContainer;
    }

    public void StartCensor()
    {
        StartCoroutine(CensorWord(1.5f));
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

    #region OnTriggerEnter
    // If enemy collides with the castle, decrease player health
    private void OnTriggerEnter(Collider other)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (other.gameObject.tag == "Castle" && enemyData.Type != EnemyType.Support && enemyData.Type != EnemyType.Support_Boss
            || other.gameObject.tag == "Player" && enemyData.Type != EnemyType.Support && enemyData.Type != EnemyType.Support_Boss)
        {
            lvlDataManager.playerCurrentHealth -= enemyData.AttackDamage;
            Debug.Log("Enemy has entered the castle");

            if (player.speakingGib)
                player.StopSpeakingGibberish();

            if (enemyData.Type == EnemyType.Boss && currentScene == "Level1")
            {
                FindObjectOfType<AudioManager>().Stop("Boss_Level_1_BGM");
            }
            else if (enemyData.Type == EnemyType.Boss && currentScene == "Level2")
            {
                FindObjectOfType<AudioManager>().Stop("Boss_Level_2_BGM");
            }

            EnemyManager.hasActiveEnemy = false;
            _enemyManager.enemyList.Remove(this);

            Destroy(this.gameObject);
        }

        //For the support enemy to move back and forth
        if (other.gameObject.tag == "Castle" && enemyData.Type == EnemyType.Support) // going left so flip when 
        {
            thisSprite.flipX = true;
            isWalking = false;
            isWalkingRight = true;
        }
        if (other.gameObject.tag == "Castle" && enemyData.Type == EnemyType.Support_Boss)
        {
            thisSprite.flipX = true;
            isWalking = false;
            isWalkingRight = true;
        }

        if (other.gameObject.tag == "RightWall" && enemyData.Type == EnemyType.Support && !isDefeat)
        {
            thisSprite.flipX = false;
            isWalking = true;
            isWalkingRight = false;
        }
        if (other.gameObject.tag == "RightWall" && enemyData.Type == EnemyType.Support_Boss && !isDefeat)
        {
            thisSprite.flipX = false;
            isWalking = true;
            isWalkingRight = false;
        }

        // Despawn enemy if isDefeat is true
        if (other.gameObject.tag == "RightWall" && isDefeat)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void TypeBox()
    {
        if (enemyData.ArmorCount == 2)
        {
            twoArmorTypeBox.enabled = true;
            oneArmorTypeBox.enabled = true;
        }
        else if (enemyData.ArmorCount == 1)
        {
            twoArmorTypeBox.enabled = false;
            oneArmorTypeBox.enabled = true;
        }
        else if (enemyData.ArmorCount == 0)
        {
            twoArmorTypeBox.enabled = false;
            oneArmorTypeBox.enabled = false;
        }
    }

    #region GetRandomWord
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

                case EnemyType.OrcCaps:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordCapsTutorial();
                    break;

                case EnemyType.Boss:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetBossWordTutorial();
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

                case EnemyType.Armored_Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;

                case EnemyType.Armored_Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;

                case EnemyType.Boss:
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

                case EnemyType.Armored_Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelTwo();
                    break;

                case EnemyType.Armored_Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelTwo();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelTwo();
                    break;

                case EnemyType.Support:
                    wordToType = WordGenerator.GetNormalWordLevelTwo();
                    break;

                case EnemyType.Support_Boss:
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

                case EnemyType.Armored_Goblin:
                    FindObjectOfType<AudioManager>().Play("Goblin_Noise_SFX");
                    wordToType = WordGenerator.GetEasyWordLevelThree();
                    break;

                case EnemyType.Armored_Orc:
                    FindObjectOfType<AudioManager>().Play("Orc_Noise_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Support:
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.Support_Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Caster_Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Caster:
                    FindObjectOfType<AudioManager>().Play("Caster_Teleport_In_SFX");
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.CasterProjectile://added this
                    wordToType = WordGenerator.GetProjectileWord();
                    break;
            }
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }
    #endregion

    #region GetRandomWordStagger
    private void generateWordStagger() //added dis cuz using generateword() will use the spawn sound again
    {
        string currentScene = SceneManager.GetActiveScene().name;
        typeIndex = 0;

        if (currentScene == "Level1")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    wordToType = WordGenerator.GetEasyWordLevelOne();
                    break;

                case EnemyType.Armored_Orc: // added dis
                    wordToType = WordGenerator.GetNormalWordLevelOne();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelOne();
                    break;

                case EnemyType.Support_Boss:
                    wordToType = WordGenerator.GetBossWordLevelOne();
                    break;
            }
        }

        if (currentScene == "Level2")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    wordToType = WordGenerator.GetEasyWordLevelTwo();
                    break;

                case EnemyType.Armored_Orc: // added dis
                    wordToType = WordGenerator.GetNormalWordLevelTwo();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelTwo();
                    break;

                case EnemyType.Support_Boss:
                    wordToType = WordGenerator.GetBossWordLevelTwo();
                    break;
            }
        }

        if (currentScene == "Level3")
        {
            switch (enemyData.Type)
            {
                case EnemyType.Armored_Goblin: // added dis
                    wordToType = WordGenerator.GetEasyWordLevelThree();
                    break;

                case EnemyType.Armored_Orc: // added dis
                    wordToType = WordGenerator.GetNormalWordLevelThree();
                    break;

                case EnemyType.Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Support_Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;

                case EnemyType.Caster_Boss:
                    wordToType = WordGenerator.GetBossWordLevelThree();
                    break;
            }
        }

        if (text != null)
            text.text = wordToType;

        wordContainer = wordToType;
    }
    #endregion
}
