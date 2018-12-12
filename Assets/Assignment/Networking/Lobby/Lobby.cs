using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour {


	void Start () {
        print("Connecting...");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}
    
	private void OnConnectedToMaster()
    {
        print("Connected");
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined Lobby");


       
    }
	
}
