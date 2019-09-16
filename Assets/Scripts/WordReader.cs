using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordReader
{
    public static int GetWordGrade(string words)
    {
        if (words.Length < 2 || words.Length > 6) return -1;
        float wordTyping = GetWordTyping(words);
        return 4 <= wordTyping && wordTyping < 7 ? 3 :
                7 <= wordTyping && wordTyping < 12 ? 2 :
                12 <= wordTyping && wordTyping < 17 ? 1 : 0;
    }

    public static float GetWordTyping(string words)
    {
        float wordTyping = 0;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] < '가' || words[i] > '힣') return -1;
            wordTyping += GetFirstWord(words[i]) + GetMiddleWord(words[i]) + GetLastWord(words[i]);
        }
        return wordTyping;
    }
    public static float GetFirstWord(char word)
    {
        int wordCode = (word - '가') / 28 / 21;
        if (wordCode == 1 || wordCode == 4 || wordCode == 8 || wordCode == 10 || wordCode == 13) return 1.3f;
        else return 1;
    }
    public static float GetMiddleWord(char word)
    {
        int wordCode = (word - '가') / 28 % 21;
        if (wordCode == 3 || wordCode == 7) return 1.3f;
        else if (wordCode == 9 || wordCode == 10 || wordCode == 11 || wordCode == 14 || wordCode == 15 || 
            wordCode == 16 || wordCode == 19) return 2;
        else return 1;
    }
    public static float GetLastWord(char word)
    {
        int wordCode = (word - '가') % 28;

        if (wordCode == 2 || wordCode == 20) return 1.3f;
        //If there is no last word
        else if (wordCode == 0) return 0;
        //If last word is combined
        else if (wordCode == 3 || wordCode == 5 || wordCode == 6 || wordCode == 9 || wordCode == 10 || 
            wordCode == 11 || wordCode == 12 || wordCode == 13 || wordCode == 14 || wordCode == 15 || 
            wordCode == 18) return 2;
        else return 1;
    }
}
