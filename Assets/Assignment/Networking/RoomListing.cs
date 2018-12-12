using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {

    [SerializeField]
    private Text roomNameText;
    private Text RoomNameText { get { return roomNameText; } }

    public bool updated { get; set; }

    public string RoomName { get; private set; }

	void Start () {
        GameObject lobbyCanvasObject = MainCanvas.Instance.LobbyCanvas.gameObject;
        if(lobbyCanvasObject == null)
        {
            return;
        }
        LobbyCanvas lobbyCanvas = lobbyCanvasObject.GetComponent<LobbyCanvas>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
	}

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void setRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }


}
