using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTools : MonoBehaviour
{
    public Text playerTypingText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerTypingText.text = "현재 타수 : " + WordSpace.inst.playerTyping;
    }
}
