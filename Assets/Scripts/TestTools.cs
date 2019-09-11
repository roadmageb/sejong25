using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTools : MonoBehaviour
{
    public InputField inputField;
    public Text brainWeight;

    public void RemoveWord()
    {
        WordSpace.inst.RemoveWord(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        brainWeight.text = "현재 무게 : " + WordSpace.inst.brainWeight;
    }
}
