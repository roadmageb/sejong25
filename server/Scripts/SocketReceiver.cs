using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

using UnityEngine.UI;

public class SocketReceiver : MonoBehaviour
{
    public Text textObj;
    public string textStr = "";

    [DllImport("__Internal")]
    private static extern void _SendData(string id, string data);

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SendData(string id, string data)
    {
        _SendData(id, data);
    }

    public void OnReceive(string value)
    {
        textStr += value + '\n';
        textObj.text = textStr;
        SendData("pong", "");
        //Debug.Log(value);
    }
}
