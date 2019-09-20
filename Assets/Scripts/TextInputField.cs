using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputField : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }
    char temp = '0';
    public List<char> tempList = new List<char>();
    bool isShifted = false;
    int previousLength = 0;
    string combinedInput = "";

    char CombinedWord(char a, char b)
    {
        string temp = a.ToString() + b.ToString();
        switch (temp)
        {
            case "ㅗㅏ": return 'ㅘ'; case "ㅗㅐ": return 'ㅙ'; case "ㅗㅣ": return 'ㅚ';
            case "ㅜㅓ": return 'ㅝ'; case "ㅜㅔ": return 'ㅞ'; case "ㅜㅣ": return 'ㅟ';
            case "ㅡㅣ": return 'ㅢ'; case "ㄱㅅ": return 'ㄳ'; case "ㄴㅈ": return 'ㄵ';
            case "ㄴㅎ": return 'ㄶ'; case "ㄹㄱ": return 'ㄺ'; case "ㄹㅁ": return 'ㄻ';
            case "ㄹㅂ": return 'ㄼ'; case "ㄹㅅ": return 'ㄽ'; case "ㄹㅌ": return 'ㄾ';
            case "ㄹㅍ": return 'ㄿ'; case "ㄹㅎ": return 'ㅀ'; case "ㅂㅅ": return 'ㅄ';
            default: return '0';
        }
    }

    bool IsVowel(char a) { return a >= 'ㅏ' && a <= 'ㅣ'; } //True for vowel, false for consonant

    string CombineInput()
    {
        List<char> fixedInput = new List<char>();
        if(combinedInput.Length > 0) combinedInput.Remove(combinedInput.Length - 1);

        for(int i = 0; i < tempList.Count; i++)
        {
            if (IsVowel(tempList[i]) && i + 1 < tempList.Count && CombinedWord(tempList[i], tempList[i + 1]) != '0')
                fixedInput.Add(CombinedWord(tempList[i], tempList[i++ + 1]));
            else if (!IsVowel(tempList[i]) && i - 1 >= 0 && i + 1 < tempList.Count && IsVowel(tempList[i - 1]) && !IsVowel(tempList[i + 1])
                && ((i + 2 >= tempList.Count) || !IsVowel(tempList[i + 2])) && CombinedWord(tempList[i], tempList[i + 1]) != '0')
                fixedInput.Add(CombinedWord(tempList[i], tempList[i++ + 1]));
            else fixedInput.Add(tempList[i]);
        }
        char first = '0', middle = '0', last = '0';
        if(fixedInput.Count > 0)
        {
            if (IsVowel(fixedInput[0])) middle = fixedInput[0];
            else first = fixedInput[0];

            for (int i = 1; i < fixedInput.Count; i++)
            {
                if (IsVowel(fixedInput[i]))
                {
                    if (!IsVowel(fixedInput[i - 1]))
                    {
                        if (last != '0')
                        {
                            last = '0';
                            combinedInput += WordProcessor.CombineWord(first, middle, last);
                            first = middle = last = '0';
                            first = fixedInput[i - 1];
                            middle = fixedInput[i];
                        }
                        else middle = fixedInput[i];
                    }
                    else
                    {
                        combinedInput += WordProcessor.CombineWord(first, middle, last);
                        first = middle = last = '0';
                        middle = fixedInput[i];
                    }
                }
                else
                {
                    if (IsVowel(fixedInput[i - 1])) last = fixedInput[i];
                    else
                    {
                        combinedInput += WordProcessor.CombineWord(first, middle, last);
                        first = middle = last = '0';
                        first = fixedInput[i];
                    }
                }
            }
            if (first != '0' || middle != '0' || last != '0') combinedInput += WordProcessor.CombineWord(first, middle, last);
        }

        tempList.Clear();
        Debug.Log(combinedInput[combinedInput.Length - 1]);
        char tempF = (char)(4352 + (combinedInput[combinedInput.Length - 1] - '가') / 28 / 21);
        char tempM = (char)(4449 + (combinedInput[combinedInput.Length - 1] - '가') / 28 % 21);
        char tempL = (char)(4520 + (combinedInput[combinedInput.Length - 1] - '가') % 28 - 1);
        Debug.Log(tempF + " : " + tempM + " : " + tempL + ":");
        if (tempF >= 'ㄱ' && tempF <= 'ㅎ') tempList.Add(tempF);
        if (tempF >= 'ㅏ' && tempF <= 'ㅣ') tempList.Add(tempM);
        if (tempF >= 'ㄱ' && tempF <= 'ㅎ') tempList.Add(tempL);
        Debug.Log(combinedInput);
        return combinedInput;
    }

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
        if (Input.GetKeyDown(KeyCode.Backspace) && tempList.Count > 0)
        {
            if (tempList.Count == 0)
            {
                combinedInput.Remove(combinedInput.Length - 1);
            }
            else
            {
                tempList.RemoveAt(tempList.Count - 1);
                CombineInput();
            }
        }
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
            CombineInput();
            temp = '0';
        }

    }
}
