using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSet : MonoBehaviour {


    [SerializeField]
    private Text playerText;

    public void OnClickSetName()
    {
        if (!PhotonNetwork.inRoom)
        {
            MainCanvas.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
        PlayerNetwork.Instance.setName(playerText.text);
        print(playerText.text);
        PhotonNetwork.player.NickName = playerText.text;
    }
}
