using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    // Tutorial Word Pool
    private static string[] tutorialEasy = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s",
                                            "t", "u", "v", "w", "x", "y", "z"   };

    private static string[] tutorialNormal = {"the", "and", "for"  };

    private static string[] tutorialNormalCaps = { "The", "End", "For" };

    private static string[] tutorialBoss = { "Hello", "Choco", "Clock" };//added this, change later



    // Level 1 Word Pool
    private static string[] lvlOneEasy = { "able", "Body", "care", "Duck", "else" };

    private static string[] lvlOneNormal = { "glory", "Heart", "items", "Juice", "knock" };

    private static string[] lvlOneBoss = { "Intelligent", "Professional", "Friendship", "Everything",
                                            "Exterminate", "intelligent", "professional", "friendship", "everything",
                                            "exterminate" };



    // Level 2 Word Pool
    private static string[] lvlTwoEasy = { "able", "Arrow", "Body", "bean", "care", "Crazy", "else"};

    private static string[] lvlTwoNormal = { "glory", "Ground", "Heart", "heaven", "items", "Juice", "knock"};

    private static string[] lvlTwoBoss = { "Basketball", "Responsible", "Astronomical", "Everything","Characters",
                                            "basketball", "responsible", "astronomical", "everything","characters"};

    private static string[] lvlTwoSupport = { "money", "Music", "Night", "noble", "other", "Ocean", "Peace", "queen"};



    // Level 3 Word Pool
    private static string[] lvlThreeEasy = { "able", "Body", "bean", "care", "Crazy", "Duck", "dream", "else", "Earth",
                                            "Fish", "flute"};

    private static string[] lvlThreeNormal = { "glory", "Ground", "Heart", "heaven", "items", "Insect", "Juice", "joyful",
                                            "knock", "Keyboard", "Legend", "learning" };

    private static string[] lvlThreeBoss = { "Vaccination", "Worshipping", "Wrongheaded", "Xenogeneic", "Yellowstone","Zygomycetes",
                                            "vaccination", "worshipping", "wrongheaded", "xenogeneic", "yellowstone","zygomycetes"};

    private static string[] lvlThreeSupport = { "money", "Music", "Night", "noble", "other", "Ocean", "Peace", "potato",
                                            "queen", "Quotes", "Reason", "river" };

    private static string[] lvlThreeCaster = { "Strawberry", "silhouette", "Technology", "television", "Understand", "university", "Vegetables", "victorious",
                                            "Wilderness", "wonderland", "Xylophone", "xenogamy" };

    // Projectile Word Pool
    private static string[] projectileWords = { "life", "ring", "wolf", "fish", "king", "bean", "evil", "peace", "heart",
                                            "south", "stone", "thing" };//added this, change later



    // Get Random Level One Word
    public static string GetEasyWordTutorial()
    {
        int randomIndex = Random.Range(0, tutorialEasy.Length);
        string randomWord = tutorialEasy[randomIndex];
        return randomWord;
    }

    public static string GetNormalWordTutorial()
    {
        int randomIndex = Random.Range(0, tutorialNormal.Length);
        string randomWord = tutorialNormal[randomIndex];
        return randomWord;
    }
    public static string GetNormalWordCapsTutorial()
    {
        int randomIndex = Random.Range(0, tutorialNormalCaps.Length);
        string randomWord = tutorialNormalCaps[randomIndex];
        return randomWord;
    }

    public static string GetBossWordTutorial()
    {
        int randomIndex = Random.Range(0, tutorialBoss.Length);
        string randomWord = tutorialBoss[randomIndex];
        return randomWord;
    }


    // Get Random Level One Word
    public static string GetEasyWordLevelOne()
    {
        int randomIndex = Random.Range(0, lvlOneEasy.Length);
        string randomWord = lvlOneEasy[randomIndex];
        return randomWord;
    }

    public static string GetNormalWordLevelOne()
    {
        int randomIndex = Random.Range(0, lvlOneNormal.Length);
        string randomWord = lvlOneNormal[randomIndex];
        return randomWord;
    }
    
    public static string GetBossWordLevelOne()
    {
        int randomIndex = Random.Range(0, lvlOneBoss.Length);
        string randomWord = lvlOneBoss[randomIndex];
        return randomWord;
    }


    // Get Random Level Two Word
    public static string GetEasyWordLevelTwo()
    {
        int randomIndex = Random.Range(0, lvlTwoEasy.Length);
        string randomWord = lvlTwoEasy[randomIndex];
        return randomWord;
    }

    public static string GetNormalWordLevelTwo()
    {
        int randomIndex = Random.Range(0, lvlTwoNormal.Length);
        string randomWord = lvlTwoNormal[randomIndex];
        return randomWord;
    }

    public static string GetBossWordLevelTwo()
    {
        int randomIndex = Random.Range(0, lvlTwoBoss.Length);
        string randomWord = lvlTwoBoss[randomIndex];
        return randomWord;
    }


    // Get Random Level Three Word
    public static string GetEasyWordLevelThree()
    {
        int randomIndex = Random.Range(0, lvlThreeEasy.Length);
        string randomWord = lvlThreeEasy[randomIndex];
        return randomWord;
    }

    public static string GetNormalWordLevelThree()
    {
        int randomIndex = Random.Range(0, lvlThreeNormal.Length);
        string randomWord = lvlThreeNormal[randomIndex];
        return randomWord;
    }

    public static string GetBossWordLevelThree()
    {
        int randomIndex = Random.Range(0, lvlThreeBoss.Length);
        string randomWord = lvlThreeBoss[randomIndex];
        return randomWord;
    }

    // Get Projectile Word
    public static string GetProjectileWord()
    {
        int randomIndex = Random.Range(0, projectileWords.Length);
        string randomWord = projectileWords[randomIndex];
        return randomWord;
    }
}
