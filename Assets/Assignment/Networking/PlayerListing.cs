using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {

	public PhotonPlayer PhotonPlayer { get; private set; }

    [SerializeField]
    private Text playerName;
    private Text PlayerName { get { return playerName; } }

    public void ApplyPhotonPlayer(PhotonPlayer photonPLayer)
    {
        PhotonPlayer = photonPLayer;
        PlayerName.text = photonPLayer.NickName;
    }
}
