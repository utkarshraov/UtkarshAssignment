﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {

    [SerializeField]
    private GameObject roomListingPrefab;
    private GameObject RoomListingPrefab { get { return roomListingPrefab; } }

    private List<RoomListing> roomListingButtons = new List<RoomListing>();
    private List<RoomListing> RoomListingButtons { get { return roomListingButtons; } }

    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach(RoomInfo room in rooms)
        {
            RoomReceived(room);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);
        if(index == -1)
        {
            if(room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObject = Instantiate(RoomListingPrefab);
              
                roomListingObject.transform.SetParent(transform,false);
                RoomListing roomListing = roomListingObject.GetComponent<RoomListing>();
                RoomListingButtons.Add(roomListing);

                index = (RoomListingButtons.Count - 1);
            }
        }
        if(index != -1)
        {
            RoomListing roomListing = RoomListingButtons[index];
            roomListing.setRoomNameText(room.Name);
            roomListing.updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach(RoomListing roomListing in RoomListingButtons)
        {
            if(!roomListing.updated)
            {
                removeRooms.Add(roomListing);
            }
            else
            {
                roomListing.updated = false;
            }
        }

        foreach(RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObject = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObject);
        }
    }
}