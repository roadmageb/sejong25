using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{


    public static void LoadImages()
    {
        for (int grade = 0; grade < 4; grade++)
            for (int length = 0; length < 5; length++)
                WordSpace.inst.wordBackgrounds[grade, length] = Resources.Load<Sprite>("WordBackgrounds/" + grade + "_" + (length + 2));
    }
    public static void ParseCSVFile()
    {
        var csvFile = Resources.Load("KKUTU_word");
        string[] splitText = csvFile.ToString().Split('\n');
        Debug.Log(':' + splitText[0] + ':');
        Debug.Log(splitText[0].Length);
        Debug.Log(WordReader.GetWordGrade(splitText[0]));
        for (int i = 0; i < splitText.Length; i++)
        {
            switch (WordReader.GetWordGrade(splitText[i]))
            {
                case 0: WordSpace.inst.stringWords[0].Add(splitText[i]); break;
                case 1: WordSpace.inst.stringWords[1].Add(splitText[i]); break;
                case 2: WordSpace.inst.stringWords[2].Add(splitText[i]); break;
                case 3: WordSpace.inst.stringWords[3].Add(splitText[i]); break;
                default: break;
            }
        }
    }
}
