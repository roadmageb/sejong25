using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
#if UNITY_EDITOR
using SocketIO;
#endif

public class SocketManager : SingletonBehaviour<SocketManager>
{
    public delegate void Callback(string id, JObject data);
    private Callback onReceieve = new Callback((string id, JObject data) => { Debug.Log("receieve " + id); });

    public JObject emptyObj = new JObject();

#if UNITY_EDITOR
    [SerializeField]
    private SocketIOComponent socket;
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
        socket.enabled = true;
#elif UNITY_WEBGL
        _ConnectServer();
#endif
    }

    private void Start()
    {
        // example of using onReceieve
        StartCoroutine(PingPongExample());
    }

    IEnumerator PingPongExample()
    {
        yield return new WaitForSeconds(1);
        AttachOnCallback("myPong", (string id, JObject data) =>
        {
            if (id == "myPong")
            {
                Debug.Log("HandShake done, Connected, " + data["str"]);
            }
            SendData("myPong", emptyObj);
        });
        SendData("myPing", emptyObj);
    }

    public void SendData(string id, JObject data)
    {
        string strData = data.ToString();
#if UNITY_EDITOR
        // TODO: socket emit
        socket.Emit(id, new JSONObject(data.ToString()));
#elif UNITY_WEBGL
        _SendData(id, strData);
#endif
    }

    public void AttachOnCallback(string id, Callback cb)
    {
#if UNITY_EDITOR
        socket.On(id, (SocketIOEvent e) =>
        {
            Debug.Log(e.name + " receieved, " + (e.data == null));
            //JObject receieve = new JObject();
            //receieve.Add("str", e.data.ToString());
            //JObject data = (e.data == null ? emptyObj : receieve);
            //Debug.Log(data.ToString());
            //cb(e.name, data);
        });
#elif UNITY_WEBGL
        onReceieve += cb;
#endif
    }

    public void OnReceieve(string value)
    {
        JObject recieved = JObject.Parse(value);
        //Debug.Log(recieved["id"].ToString() + recieved["data"].ToString());
        onReceieve(recieved["id"].ToString(), JObject.Parse(recieved["data"].ToString()));
    }
}