using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

public class SocketManager : SingletonBehaviour<SocketManager>
{
    public delegate void callback(string id, JObject data);
    public callback onReceieve = new callback((string id, JObject data) => { Debug.Log("recieve " + id); });

    public JObject emptyObj = new JObject();

#if UNITY_EDITOR
    public string url = "http://127.0.0.1/";
#elif UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void _SendData(string id, string data);
    [DllImport("__Internal")]
    private static extern void _ConnectServer();
#endif

    private void Awake()
    {
        DontDestroyOnLoad(this);
#if UNITY_EDITOR
        // TODO: socket connect
#elif UNITY_WEBGL
        _ConnectServer();
#endif
    }

    private void Start()
    {
        // example of using onReceieve
        onReceieve += (string id, JObject data) =>
        {
            if (id == "myPong")
            {
                Debug.Log("HandShake done, Connected");
            }
        };
        SendData("myPing", emptyObj);
    }

    public void SendData(string id, JObject data)
    {
        string strData = data.ToString();
#if UNITY_EDITOR
        // TODO: socket emit
#elif UNITY_WEBGL
        _SendData(id, strData);
#endif
    }

    public void OnReceieve(string value)
    {
        JObject recieved = JObject.Parse(value);
        //Debug.Log(recieved["id"].ToString() + recieved["data"].ToString());
        onReceieve(recieved["id"].ToString(), JObject.Parse(recieved["data"].ToString()));
    }
}