﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

public class SocketManager : SingletonBehaviour<SocketManager>
{
    public delegate void callback(string id, JObject data);
    public callback onReceieve = new callback((string id, JObject data) => { Debug.Log("recieve " + id); });

    public JObject emptyObj = new JObject();

    [DllImport("__Internal")]
    private static extern void _SendData(string id, string data);
    [DllImport("__Internal")]
    private static extern void _ConnectServer();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _ConnectServer();
    }

    private void Start()
    {
        // example of using onReceieve
        onReceieve += (string id, JObject data) =>
        {
            if (id == "myPing")
            {
                Debug.Log(id + ": " + data.ToString());
                SendData("myPong", emptyObj);
            }
        };
    }

    public void SendData(string id, JObject data)
    {
        string strData = data.ToString();
        _SendData(id, strData);
    }

    public void OnReceieve(string value)
    {
        JObject recieved = JObject.Parse(value);
        onReceieve(recieved["id"].ToString(), JObject.Parse(recieved["data"].ToString()));
    }
}