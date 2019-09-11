using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser : MonoBehaviour
{


    void Awake()
    {
        for (int i = 0; i < 4; i++) WordSpace.inst.stringWords[i] = new List<string>();

        var csvFile = Resources.Load("KKUTU_word.csv");

    }
}
