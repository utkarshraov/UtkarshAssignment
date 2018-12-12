using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour {

    [SerializeField]
    private GameObject playerListingPrefab;
    private GameObject PlayerListingPrefab { get { return playerListingPrefab; } }

    private List<PlayerListing> playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings { get { return playerListings; } }


    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }

    private void OnJoinedRoom()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        MainCanvas.Instance.CurrentRoomCanvas.transform.SetAsLastSibling();

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        for(int i=0;i<photonPlayers.Length;i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);
        GameObject playerListObject = Instantiate(PlayerListingPrefab);
        playerListObject.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListObject.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        playerListings.Add(playerListing);
    }
    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if(index!=-1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }

    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
