using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    // 3-6 letter words
    private static string[] wordListEasy = { "fuel", "trap", "cars", "answer", "boil", "cactus", "icy",  "suck", "songs",
                                        "spotty", "invent", "cake", "employ", "linen", "copy", "fasten", "fix", "goofy",
                                        "title", "rabid", "ritzy", "locket", "plot", "erect", "soft", "cuddly", "nerve",
                                        "ball", "fly", "gratis", "solid", "cable", "wrench", "known", "feeble", "animal"    };

    //// 7-8 letter words
    private static string[] wordListNormal = { "elephant", "parallel", "hanging", "partner", "shelter", "cushion", "undress",
                                        "purring", "thought", "pharaoh", "nauseous", "jacuzzi", "strange", "session", "promise",
                                        "morning", "popular", "minister", "property", "engineer", "division", "reckless"    };

    // 9+ letter words
    private static string[] wordListHard = { "heartbreaking", "schizophrenia", "unwritten", "rainstorm", "boundless", "volleyball", "basketball",
                                        "handkerchief", "accommodate", "orangutan", "mischievous", "government", "interface", "challenge",
                                        "notorious", "allowance", "tournament", "helicopter", "convulsion", "established", "responsible",
                                        "performance", "calculation", "cooperative", "democratic", "motorcycle", "substitute"   };

    private static string[] playerWordList = { "absolute", "ninja", "justice", "guitar", "inject", "grace", "famine", "mouse", "core",
                                        "locket", "coin" };

    public static string GetEasyWord()
    {
        int randomIndex = Random.Range(0, wordListEasy.Length);
        string randomWord = wordListEasy[randomIndex];
        return randomWord;
    }

    public static string GetNormalWord()
    {
        int randomIndex = Random.Range(0, wordListNormal.Length);
        string randomWord = wordListNormal[randomIndex];
        return randomWord;
    }

    public static string GetHardWord()
    {
        int randomIndex = Random.Range(0, wordListHard.Length);
        string randomWord = wordListHard[randomIndex];
        return randomWord;
    }

    public static string GetPlayerWord()
    {
        int randomIndex = Random.Range(0, playerWordList.Length);
        string randomWord = playerWordList[randomIndex];
        return randomWord;
    }
}
