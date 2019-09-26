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
    public delegate void Callback(JObject data);
    private Dictionary<string, Callback> onReceieve = new Dictionary<string, Callback>();

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
        socket.enabled = true;
#elif UNITY_WEBGL
        _ConnectServer();
#endif
    }

    private void Start()
    {
        StartCoroutine(PingPongExample());
    }

    // example of using onReceieve
    IEnumerator PingPongExample()
    {
        yield return new WaitForSeconds(1);
        SocketOn("myPong", (JObject data) =>
        {
            Debug.Log("HandShake done, Connected, " + data["hello"].ToString());
            SocketEmit("myPong", JObject.Parse("{ res: \"Hello Server!\" }"));
        });
        SocketEmit("myPing", emptyObj);
    }

    public void SocketEmit(string id, JObject data)
    {
        string strData = data.ToString();
#if UNITY_EDITOR
        socket.Emit(id, new JSONObject(strData));
        Debug.Log("emit: " + id);
#elif UNITY_WEBGL
        _SendData(id, strData);
#endif
    }

    public void SocketOn(string id, Callback cb)
    {
#if UNITY_EDITOR
        socket.On("data", (SocketIOEvent e) =>
        {
            Debug.Log("receive: " + e.data.GetField("id").str);
            cb(JObject.Parse(e.data.GetField("data").ToString()));
        });
#elif UNITY_WEBGL
        onReceieve.Add(id, cb);
#endif
    }

    public void OnReceive(string value)
    {
        JObject received = JObject.Parse(value);
        //Debug.Log(recieved["id"].ToString() + recieved["data"].ToString());
        onReceieve[received["id"].ToString()](JObject.Parse(received["data"].ToString()));
    }
}