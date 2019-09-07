﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputField : MonoBehaviour
{
    public InputField inputField = null;
    public TextMesh resultText = null;

    // Start is called before the first frame update
    void Start()
    {
        inputField.characterLimit = 6;
    }
    char temp = '0';
    List<char> tempList = new List<char>();
    bool isShifted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) temp = 'ㅁ'; else if (Input.GetKeyDown(KeyCode.B)) temp = 'ㅠ'; else if (Input.GetKeyDown(KeyCode.C)) temp = 'ㅊ';
        else if (Input.GetKeyDown(KeyCode.D)) temp = 'ㅇ'; else if (Input.GetKeyDown(KeyCode.E)) temp = 'ㄷ'; else if (Input.GetKeyDown(KeyCode.F)) temp = 'ㄹ';
        else if (Input.GetKeyDown(KeyCode.G)) temp = 'ㅎ'; else if (Input.GetKeyDown(KeyCode.H)) temp = 'ㅗ'; else if (Input.GetKeyDown(KeyCode.I)) temp = 'ㅑ';
        else if (Input.GetKeyDown(KeyCode.J)) temp = 'ㅓ'; else if (Input.GetKeyDown(KeyCode.K)) temp = 'ㅏ'; else if (Input.GetKeyDown(KeyCode.L)) temp = 'ㅣ';
        else if (Input.GetKeyDown(KeyCode.M)) temp = 'ㅡ'; else if (Input.GetKeyDown(KeyCode.N)) temp = 'ㅜ'; else if (Input.GetKeyDown(KeyCode.O)) temp = 'ㅐ';
        else if (Input.GetKeyDown(KeyCode.P)) temp = 'ㅔ'; else if (Input.GetKeyDown(KeyCode.Q)) temp = 'ㅂ'; else if (Input.GetKeyDown(KeyCode.R)) temp = 'ㄱ';
        else if (Input.GetKeyDown(KeyCode.S)) temp = 'ㄴ'; else if (Input.GetKeyDown(KeyCode.T)) temp = 'ㅅ'; else if (Input.GetKeyDown(KeyCode.U)) temp = 'ㅕ';
        else if (Input.GetKeyDown(KeyCode.V)) temp = 'ㅍ'; else if (Input.GetKeyDown(KeyCode.W)) temp = 'ㅈ'; else if (Input.GetKeyDown(KeyCode.X)) temp = 'ㅌ';
        else if (Input.GetKeyDown(KeyCode.Y)) temp = 'ㅛ'; else if (Input.GetKeyDown(KeyCode.Z)) temp = 'ㅋ';
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) isShifted = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) isShifted = false;
        if (isShifted)
        {
            switch (temp)
            {
                case 'ㄱ': temp = 'ㄲ'; break;
                case 'ㄷ': temp = 'ㄸ'; break;
                case 'ㅂ': temp = 'ㅃ'; break;
                case 'ㅅ': temp = 'ㅆ'; break;
                case 'ㅈ': temp = 'ㅉ'; break;
                case 'ㅐ': temp = 'ㅒ'; break;
                case 'ㅔ': temp = 'ㅖ'; break;
                default: break;
            }
        }
        if (temp != '0')
        {
            tempList.Add(temp);
            temp = '0';
        }
        string debugText = "";
        for (int i = 0; i < tempList.Count; i++)
        {
            debugText += tempList[i];
        }
        Debug.Log(debugText);
    }
}
