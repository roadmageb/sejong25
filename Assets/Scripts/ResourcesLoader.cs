using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{


    public static void LoadImages()
    {
        for (int grade = 0; grade < 4; grade++)
            for (int length = 2; length < 7; length++)
                WordSpace.inst.wordBackgrounds[grade, length - 2] = Resources.Load<Sprite>("WordBackgrounds/" + grade + "_" + length);
        for(int length = 2; length < 7; length++)
            WordSpace.inst.hopaeBackgrounds[length - 2] = Resources.Load<Sprite>("hopaeBAckgrounds/name" + length);
    }
    public static void ParseCSVFile()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("KKUTU_word");
        for (int i = 0; i < data.Count; i++)
        {
            var text = data[i]["Text"].ToString();
            switch (WordProcessor.GetWordGrade(text))
            {
                case 0: WordSpace.inst.stringWords[0].Add(text); break;
                case 1: WordSpace.inst.stringWords[1].Add(text); break;
                case 2: WordSpace.inst.stringWords[2].Add(text); break;
                case 3: WordSpace.inst.stringWords[3].Add(text); break;
                default: break;
            }
        }
    }
}
