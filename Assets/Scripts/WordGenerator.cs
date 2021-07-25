using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    // Tutorial Word Pool
    private static string[] tutorialEasy = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s",
                                            "t", "u", "v", "w", "x", "y", "z"   };

    private static string[] tutorialNormal = {"one", "day", "you"  };

    private static string[] tutorialNormalCaps = { "One", "Day", "You" };

    private static string[] tutorialBoss = { "Hello", "Choco", "Clock" };//added this, change later



    // Level 1 Word Pool
    private static string[] lvlOneEasy = { "Life", "Ring", "Wolf", "Fish", "King" , "life", "ring", "wolf", "fish", "king" };

    private static string[] lvlOneNormal = { "World", "Heart", "Apple", "Stone", "Smart", "world", "heart", "apple", "stone", "smart" };

    private static string[] lvlOneBoss = { "Intelligent", "Professional", "Friendship", "Everything",
                                            "Exterminate", "intelligent", "professional", "friendship", "everything",
                                            "exterminate" };



    // Level 2 Word Pool
    private static string[] lvlTwoEasy = { "Life", "Ring", "Wolf", "Fish", "King", "Bean", "Evil", "Peace", "life", "ring", "wolf", "fish", "king", "bean", "evil", "peace" };

    private static string[] lvlTwoNormal = { "World", "Heart", "Apple", "Stone", "Smart", "Average", "Twelve", "Medieval", 
                                                "world", "heart", "apple", "stone", "smart", "average", "twelve", "medieval"};

    private static string[] lvlTwoBoss = { "Intelligent", "Professional", "Friendship", "Everything",
                                            "Exterminate", "Responsible", "Astronomical",
                                                "intelligent", "professional", "friendship", "everything",
                                                    "exterminate", "responsible", "astronomical"   };



    // Level 3 Word Pool
    private static string[] lvlThreeEasy = { "Life", "Ring", "Wolf", "Fish", "King", "Bean", "Evil", "Peace", "Heart",
                                            "South", "Stone", "Thing",
                                            "life", "ring", "wolf", "fish", "king", "bean", "evil", "peace", "heart",
                                            "south", "stone", "thing"  };

    private static string[] lvlThreeNormal = { "World", "Heart", "Apple", "Stone", "Smart", "Average", "Twelve", "Medieval", 
                                            "Strength", "Donate", "Mountain", "Treasure",
                                            "world", "heart", "apple", "stone", "smart", "average", "twelve", "medieval",
                                            "strength", "donate", "mountain", "treasure"};

    private static string[] lvlThreeBoss = { "Intelligent", "Professional", "Friendship", "Everything", "Basketball",
                                            "Exterminate", "Responsible", "Astronomical", "Characters", "Technology", "Handkerchief",
                                            "intelligent", "professional", "friendship", "everything", "basketball",
                                            "exterminate", "responsible", "astronomical", "characters", "technology", "handkerchief",   };



    // Projectile Word Pool
    private static string[] projectileWords = { "Easy", "Medium", "Hard" };//added this, change later



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
