using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour {

    [SerializeField]
    private Text roomName;
    public void OnClickStartGame()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(4);
    }

    public void setName(string name)
    {
        roomName.text = name;
    }

}
