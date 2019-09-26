using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordProcessor
{
    public static int GetEditDistance(string text1, string text2) //Get edit distance between text1 and text2 by Levenshtein distance
    {
        List<char> dividedText1 = new List<char>();
        List<char> dividedText2 = new List<char>();
        for (int i = 0; i < text1.Length; i++)
        {
            dividedText1.Add((char)((text1[i] - '가') / 28 / 21 + 0x1100));
            dividedText1.Add((char)((text1[i] - '가') / 28 % 21 + 0x1161));
            dividedText1.Add((char)((text1[i] - '가') % 28 + 0x11a8));
        }
        for (int i = 0; i < text2.Length; i++)
        {
            dividedText2.Add((char)((text2[i] - '가') / 28 / 21 + 0x1100));
            dividedText2.Add((char)((text2[i] - '가') / 28 % 21 + 0x1161));
            dividedText2.Add((char)((text2[i] - '가') % 28 + 0x11a8));
        }

        var dividedText1Length = dividedText1.Count;
        var dividedText2Length = dividedText2.Count;
        var matrix = new int[dividedText1Length + 1, dividedText2Length + 1];
        if (dividedText1Length == 0) return dividedText2Length;
        if (dividedText2Length == 0) return dividedText1Length;

        for (var i = 0; i <= dividedText1Length; matrix[i, 0] = i++) { }
        for (var j = 0; j <= dividedText2Length; matrix[0, j] = j++) { }

        for (var i = 1; i <= dividedText1Length; i++)
            for (var j = 1; j <= dividedText2Length; j++)
            {
                var cost = (dividedText2[j - 1] == dividedText1[i - 1]) ? 0 : 1;
                matrix[i, j] = Mathf.Min(Mathf.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
            }
        return matrix[dividedText1Length, dividedText2Length];
    }
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

    public static char CombineWord(char first, char middle, char last)
    {
        if (first == '0') return middle;
        else if (middle == '0') return first;
        else
        {
            int firstCode = 0, lastCode = 0;
            switch (first)
            {
                case 'ㄱ': firstCode = 0; break; case 'ㄲ': firstCode = 1; break; case 'ㄴ': firstCode = 2; break;
                case 'ㄷ': firstCode = 3; break; case 'ㄸ': firstCode = 4; break; case 'ㄹ': firstCode = 5; break;
                case 'ㅁ': firstCode = 6; break; case 'ㅂ': firstCode = 7; break; case 'ㅃ': firstCode = 8; break;
                case 'ㅅ': firstCode = 9; break; case 'ㅆ': firstCode = 10; break; case 'ㅇ': firstCode = 11; break;
                case 'ㅈ': firstCode = 12; break; case 'ㅉ': firstCode = 13; break; case 'ㅊ': firstCode = 14; break;
                case 'ㅋ': firstCode = 15; break; case 'ㅌ': firstCode = 16; break; case 'ㅍ': firstCode = 17; break;
                case 'ㅎ': firstCode = 18; break;
            }
            switch (last)
            {
                case '0': lastCode = 0; break; case 'ㄱ': lastCode = 1; break; case 'ㄲ': lastCode = 2; break;
                case 'ㄳ': lastCode = 3; break; case 'ㄴ': lastCode = 4; break; case 'ㄵ': lastCode = 5; break;
                case 'ㄶ': lastCode = 6; break; case 'ㄷ': lastCode = 7; break; case 'ㄹ': lastCode = 8; break;
                case 'ㄺ': lastCode = 9; break; case 'ㄻ': lastCode = 10; break; case 'ㄼ': lastCode = 11; break;
                case 'ㄽ': lastCode = 12; break; case 'ㄾ': lastCode = 13; break; case 'ㄿ': lastCode = 14; break;
                case 'ㅀ': lastCode = 15; break; case 'ㅁ': lastCode = 16; break; case 'ㅂ': lastCode = 17; break;
                case 'ㅄ': lastCode = 18; break; case 'ㅅ': lastCode = 19; break; case 'ㅆ': lastCode = 20; break;
                case 'ㅇ': lastCode = 21; break; case 'ㅈ': lastCode = 22; break; case 'ㅊ': lastCode = 23; break;
                case 'ㅋ': lastCode = 24; break; case 'ㅌ': lastCode = 25; break; case 'ㅍ': lastCode = 26; break;
                case 'ㅎ': lastCode = 27; break;
            }
            return (char)('가' + 28 * 21 * firstCode + 28 * (middle - 'ㅏ') + lastCode);
        }
    }
}
