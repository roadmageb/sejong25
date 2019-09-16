using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    string _userName = "홍길동"; //Name of this player
    public string UserName { get => _userName; set => _userName = value; }


    int _userID = 12345;
    public int UserID { get => _userID; }

}