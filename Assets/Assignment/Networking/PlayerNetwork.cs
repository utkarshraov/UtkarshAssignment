using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

    private int PlayersInGame = 0;

    private int index = 0;

    private PhotonView PhotonView;

    public void setName(string name)
    {
        PlayerName = name;
    }
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        if(scene.name == "WarlocksGame")
        {
            if(PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPCLoadedGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPCLoadGameOthers", PhotonTargets.Others);
        
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPCLoadedGameScene", PhotonTargets.MasterClient);
    }

    [PunRPC]
    private void RPCLoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPCLoadedGameScene()
    {
        PlayersInGame++;
        if(PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players finished loading");
            PhotonView.RPC("RPCStartGame", PhotonTargets.MasterClient);
            PhotonView.RPC("RPCCreatePlayer", PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPCCreatePlayer()
    {
        createPlayer(index++);
    }

    [PunRPC]
    private void RPCStartGame()
    {
        Game.instance.StartGame();
    }


    private void createPlayer(int i)
    {
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "LocalPlayer"), Vector3.zero, Quaternion.identity, 0);
        player.transform.SetParent(NetworkPlayerLoader.instance.playerParent);
        player.transform.name = "Player" + i;
        player.GetComponent<LocalPlayerController>().Gamee = NetworkPlayerLoader.instance.game;
        player.GetComponent<LocalPlayerController>().inGameUi = NetworkPlayerLoader.instance.inGameUi;
        player.GetComponent<LocalPlayerController>().inputManager = NetworkPlayerLoader.instance.inputManager;
        player.GetComponent<LocalPlayerController>().map = NetworkPlayerLoader.instance.map;
        player.GetComponent<LocalPlayerController>().eventSystem = NetworkPlayerLoader.instance.eventsystem;
        player.GetComponent<LocalPlayerController>().graphicRaycaster = NetworkPlayerLoader.instance.graphicRaycaster;
        NetworkPlayerLoader.instance.game.Players.Add(player.GetComponent<Player>());

        player.GetComponent<Player>().Color = NetworkPlayerLoader.instance.colors[i];

        GameObject unit = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Unit"), Vector3.zero, Quaternion.identity, 0);
        unit.transform.name = "Unit" + (i + 1);
        unit.GetComponent<Unit>().Owner = player.GetComponent<Player>();
        player.GetComponent<LocalPlayerController>().units.Add(unit.GetComponent<Unit>());
        NetworkPlayerLoader.instance.units.Add(unit.GetComponent<Unit>());
        player.GetComponent<LocalPlayerController>().SelectedUnit = player.GetComponent<LocalPlayerController>().units[0];
        unit.GetComponent<UnitMovement>().Mapp = NetworkPlayerLoader.instance.map;

        NetworkPlayerLoader.instance.abiltiesHud.SetUnit(unit.GetComponent<Unit>(), true);
        NetworkPlayerLoader.instance.abiltiesHud.CurrentPlayer = player.GetComponent<Player>();
        NetworkPlayerLoader.instance.storeController.setPlayer(unit.GetComponent<Unit>(), player.GetComponent<PlayerGameStats>());
        NetworkPlayerLoader.instance.storeController.realStart();
    }
}
