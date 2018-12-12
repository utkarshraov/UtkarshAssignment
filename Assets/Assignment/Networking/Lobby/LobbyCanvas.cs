using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

    [SerializeField]
    private RoomLayoutGroup roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup { get { return roomLayoutGroup; } }
    
   public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {

        }
        else
        {
            print("failed to join room");
        }
        
    }
}
