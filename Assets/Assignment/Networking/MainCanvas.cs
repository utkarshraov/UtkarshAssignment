using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainCanvas : MonoBehaviour {

    public static MainCanvas Instance;
    [SerializeField]
    private LobbyCanvas lobbyCanvas;
    public LobbyCanvas LobbyCanvas { get { return lobbyCanvas; } }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(1);
    }
}
