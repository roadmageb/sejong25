using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputField : MonoBehaviour
{
    public delegate void Callback();
    private Callback EnterCallback = null;
    public int maxInput;

    public TextMesh inputText;
    char tempWord = '0'; //Character of input
    List<char> rawInput = new List<char>(); //List of characters from input, not combined
    bool isShifted = false;
    public string combinedInput = ""; //Result of combined input

    bool isBackspacePressed = false; //Is backskpace is pressed
    float previousRemovedTime = -1, removeDiff = 1; //Time diff of removing


    char CombinedWord(char a, char b) //Combine if vowel or consonant is combinable or return 0
    {
        string tempWord = a.ToString() + b.ToString();
        switch (tempWord)
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

    void CombineInput(char newInput = '0') //Combine raw input to combined Korean text
    {
        List<char> fixedInput = new List<char>();
        string temp = combinedInput;
        if (temp.Length > 0 && rawInput.Count > 0) temp = temp.Substring(0, temp.Length - 1);
        if (newInput != '0') rawInput.Add(newInput);

        for (int i = 0; i < rawInput.Count; i++)
        {
            if (IsVowel(rawInput[i]) && i + 1 < rawInput.Count && CombinedWord(rawInput[i], rawInput[i + 1]) != '0')
                fixedInput.Add(CombinedWord(rawInput[i], rawInput[i++ + 1]));
            else if (!IsVowel(rawInput[i]) && i - 1 >= 0 && i + 1 < rawInput.Count && IsVowel(rawInput[i - 1]) && !IsVowel(rawInput[i + 1])
                && ((i + 2 >= rawInput.Count) || !IsVowel(rawInput[i + 2])) && CombinedWord(rawInput[i], rawInput[i + 1]) != '0')
                fixedInput.Add(CombinedWord(rawInput[i], rawInput[i++ + 1]));
            else fixedInput.Add(rawInput[i]);
        }
        char first = '0', middle = '0', last = '0';
        char lastFirst = '0', lastMiddle = '0', lastLast = '0';
        if (fixedInput.Count > 0)
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
                            char tempFirst = '0';

                            switch (last)
                            {
                                case 'ㄳ': last = 'ㄱ'; tempFirst = 'ㅅ'; break;
                                case 'ㄵ': last = 'ㄴ'; tempFirst = 'ㅈ'; break;
                                case 'ㄶ': last = 'ㄴ'; tempFirst = 'ㅎ'; break;
                                case 'ㄺ': last = 'ㄹ'; tempFirst = 'ㄱ'; break;
                                case 'ㄻ': last = 'ㄹ'; tempFirst = 'ㅁ'; break;
                                case 'ㄼ': last = 'ㄹ'; tempFirst = 'ㅂ'; break;
                                case 'ㄽ': last = 'ㄹ'; tempFirst = 'ㅅ'; break;
                                case 'ㄾ': last = 'ㄹ'; tempFirst = 'ㅌ'; break;
                                case 'ㄿ': last = 'ㄹ'; tempFirst = 'ㅍ'; break;
                                case 'ㅀ': last = 'ㄹ'; tempFirst = 'ㅎ'; break;
                                case 'ㅄ': last = 'ㅂ'; tempFirst = 'ㅅ'; break;
                                default: last = '0'; break;
                            }
                            temp += WordProcessor.CombineWord(first, middle, last);
                            lastFirst = first; lastMiddle = middle; lastLast = last;
                            first = middle = last = '0';
                            first = tempFirst == '0' ? fixedInput[i - 1] : tempFirst;
                            middle = fixedInput[i];
                        }
                        else middle = fixedInput[i];
                    }
                    else
                    {
                        temp += WordProcessor.CombineWord(first, middle, last);
                        lastFirst = first; lastMiddle = middle; lastLast = last;
                        first = middle = last = '0';
                        middle = fixedInput[i];
                    }
                }
                else
                {
                    if (IsVowel(fixedInput[i - 1])) last = fixedInput[i];
                    else
                    {
                        temp += WordProcessor.CombineWord(first, middle, last);
                        lastFirst = first; lastMiddle = middle; lastLast = last;
                        first = middle = last = '0';
                        first = fixedInput[i];
                    }
                }
            }
            if (first != '0' || middle != '0' || last != '0')
            {
                temp += WordProcessor.CombineWord(first, middle, last);
                lastFirst = first; lastMiddle = middle; lastLast = last;
            }
        }
        if (temp.Length > maxInput)
        {
            rawInput.RemoveAt(rawInput.Count - 1);
            return;
        }
        rawInput.Clear();
        if (lastFirst != '0') rawInput.Add(lastFirst);
        if (lastMiddle != '0') rawInput.Add(lastMiddle);
        if (lastLast != '0') rawInput.Add(lastLast);
        inputText.text = combinedInput = temp;
    }

    public void SetCallback(Callback _callback) { EnterCallback = _callback; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) tempWord = 'ㅁ'; else if (Input.GetKeyDown(KeyCode.B)) tempWord = 'ㅠ'; else if (Input.GetKeyDown(KeyCode.C)) tempWord = 'ㅊ';
        else if (Input.GetKeyDown(KeyCode.D)) tempWord = 'ㅇ'; else if (Input.GetKeyDown(KeyCode.E)) tempWord = 'ㄷ'; else if (Input.GetKeyDown(KeyCode.F)) tempWord = 'ㄹ';
        else if (Input.GetKeyDown(KeyCode.G)) tempWord = 'ㅎ'; else if (Input.GetKeyDown(KeyCode.H)) tempWord = 'ㅗ'; else if (Input.GetKeyDown(KeyCode.I)) tempWord = 'ㅑ';
        else if (Input.GetKeyDown(KeyCode.J)) tempWord = 'ㅓ'; else if (Input.GetKeyDown(KeyCode.K)) tempWord = 'ㅏ'; else if (Input.GetKeyDown(KeyCode.L)) tempWord = 'ㅣ';
        else if (Input.GetKeyDown(KeyCode.M)) tempWord = 'ㅡ'; else if (Input.GetKeyDown(KeyCode.N)) tempWord = 'ㅜ'; else if (Input.GetKeyDown(KeyCode.O)) tempWord = 'ㅐ';
        else if (Input.GetKeyDown(KeyCode.P)) tempWord = 'ㅔ'; else if (Input.GetKeyDown(KeyCode.Q)) tempWord = 'ㅂ'; else if (Input.GetKeyDown(KeyCode.R)) tempWord = 'ㄱ';
        else if (Input.GetKeyDown(KeyCode.S)) tempWord = 'ㄴ'; else if (Input.GetKeyDown(KeyCode.T)) tempWord = 'ㅅ'; else if (Input.GetKeyDown(KeyCode.U)) tempWord = 'ㅕ';
        else if (Input.GetKeyDown(KeyCode.V)) tempWord = 'ㅍ'; else if (Input.GetKeyDown(KeyCode.W)) tempWord = 'ㅈ'; else if (Input.GetKeyDown(KeyCode.X)) tempWord = 'ㅌ';
        else if (Input.GetKeyDown(KeyCode.Y)) tempWord = 'ㅛ'; else if (Input.GetKeyDown(KeyCode.Z)) tempWord = 'ㅋ';
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) isShifted = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) isShifted = false;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterCallback();
            combinedInput = "";
            rawInput.Clear();
            inputText.text = "";
        }
        if (Input.GetKey(KeyCode.Backspace) && combinedInput.Length > 0)
        {
            if ((isBackspacePressed && Time.time - previousRemovedTime > removeDiff) || !isBackspacePressed)
            {
                if (rawInput.Count == 0)
                {
                    combinedInput = combinedInput.Substring(0, combinedInput.Length - 1);
                    inputText.text = combinedInput;
                }
                else
                {
                    rawInput.RemoveAt(rawInput.Count - 1);
                    if (rawInput.Count == 0)
                    {
                        combinedInput = combinedInput.Substring(0, combinedInput.Length - 1);
                        inputText.text = combinedInput;
                    }
                    else CombineInput();
                }
                removeDiff = isBackspacePressed ? 0.02f : 0.5f;
                previousRemovedTime = Time.time;
            }
            isBackspacePressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Backspace))
        {
            isBackspacePressed = false;
            removeDiff = 0.5f;
            previousRemovedTime = -1;
        }
        if (isShifted)
        {
            switch (tempWord)
            {
                case 'ㄱ': tempWord = 'ㄲ'; break;
                case 'ㄷ': tempWord = 'ㄸ'; break;
                case 'ㅂ': tempWord = 'ㅃ'; break;
                case 'ㅅ': tempWord = 'ㅆ'; break;
                case 'ㅈ': tempWord = 'ㅉ'; break;
                case 'ㅐ': tempWord = 'ㅒ'; break;
                case 'ㅔ': tempWord = 'ㅖ'; break;
                default: break;
            }
        }
        if (tempWord != '0')
        {
            CombineInput(tempWord);
            tempWord = '0';
        }
    }
    void LateUpdate()
    {
        WordSpace.inst.koreanInput = combinedInput;
    }
}
