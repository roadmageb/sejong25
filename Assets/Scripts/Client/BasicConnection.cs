using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class BasicConnection : MonoBehaviour
{
    private SocketManager socket;

    private void Awake()
    {
        socket = SocketManager.inst;
    }

    public void JoinRoom(int roomId)
    {
        JObject emitData = new JObject();
        emitData.Add("roomId", roomId);
        emitData.Add("hopaeName", "홍길동");
        socket.SocketEmit("joinRoom", emitData);
    }

    public void ExitRoom()
    {
        socket.SocketEmit("exitRoom", socket.emptyObj);
    }
}
