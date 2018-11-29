using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLoader : MonoBehaviour {

    [SerializeField]
    private Game game = null;

    [SerializeField]
    private InGameUi inGameUi = null;

    [SerializeField]
    private AbilitiesHud abiltiesHud = null;

    [SerializeField]
    private InputManager inputManager = null;

    [SerializeField]
    private Map map = null;

    [SerializeField]
    private EventSystem eventsystem = null;

    [SerializeField]
    private GraphicRaycaster graphicRaycaster = null;

    [SerializeField]
    private Color[] colors = null;

    [SerializeField]
    private int numPlayers = 0;

    [SerializeField]
    private GameObject unitPrefab = null;

    [SerializeField]
    private GameObject localPlayerPrefab = null;

    [SerializeField]
    private GameObject AggressiveAIPrefab = null;

    [SerializeField]
    private GameObject PlaymakerAIPrefab = null;

    [SerializeField]
    private GameObject TankAIPrefab = null;

    [SerializeField]
    private StoreController storeController = null;

    [SerializeField]
    private Transform playerParent = null;

    List<Player> players = new List<Player>();
    List<Unit> units = new List<Unit>();


    public void spawnPlayers()
    {
        GameInformation gameInformation = FindObjectOfType<GameInformation>();
        if (gameInformation == null) // default values that can spawn players
        {
            gameInformation = gameObject.AddComponent<GameInformation>();
            gameInformation.NumberOfPlayers = numPlayers;
            gameInformation.instancePCs(numPlayers);
            gameInformation.setPlayerController(0, 0);
            for (int i =1;i<numPlayers;i++)
            {
                //choose a random AI for each other unit
                gameInformation.setPlayerController(i, Random.Range(1, 4));
                gameInformation.setPLayerDifficulty(i, 1);
            }          
        }


       
        for (int i = 0; i < gameInformation.NumberOfPlayers; i++)
        {

            //iterate through the list of player information and spawn the appropriate type of AI. this manages all the scene hookups
            if (gameInformation.getPlayerController(i) == 0)
            {
                loadPlayer(i);
            }
            else if (gameInformation.getPlayerController(i) == 1)
            {
                LoadAggressiveAI(i);
                if(gameInformation.returnDifficulty(i) == 0)
                {
                    players[i].GetComponent<AggressiveAIController>().ReactionTime = 5f;
                }
            }
            else if (gameInformation.getPlayerController(i) == 2)
            {
                LoadPlayMakerAI(i);
                if (gameInformation.returnDifficulty(i) == 0)
                {
                    players[i].GetComponent<PlaymakerAIController>().ReactionTime = 5f;
                }
            }
            else if (gameInformation.getPlayerController(i) == 3)
            {
                LoadTankAI(i);
                if (gameInformation.returnDifficulty(i) == 0)
                {
                    players[i].GetComponent<TankAIController>().ReactionTime = 5f;
                }
            }
        }

        game.Players = players;
        game.Units = units;

    }

    void loadPlayer(int i)
    {
        GameObject player = Instantiate(localPlayerPrefab);
        player.transform.SetParent(playerParent);
        player.transform.name = "Local Player";
        player.GetComponent<LocalPlayerController>().Gamee = game;
        player.GetComponent<LocalPlayerController>().inGameUi = inGameUi;
        player.GetComponent<LocalPlayerController>().inputManager = inputManager;
        player.GetComponent<LocalPlayerController>().map = map;
        player.GetComponent<LocalPlayerController>().eventSystem = eventsystem;
        player.GetComponent<LocalPlayerController>().graphicRaycaster = graphicRaycaster;
        players.Add(player.GetComponent<Player>());
        

        player.GetComponent<Player>().Color = colors[i];
        GameObject unit = Instantiate(unitPrefab);
        
        unit.transform.name = "Unit" + (i + 1);
        unit.GetComponent<Unit>().Owner = player.GetComponent<Player>();
        player.GetComponent<LocalPlayerController>().units.Add(unit.GetComponent<Unit>());
        units.Add(unit.GetComponent<Unit>());
        player.GetComponent<LocalPlayerController>().SelectedUnit = player.GetComponent<LocalPlayerController>().units[0];
        unit.GetComponent<UnitMovement>().Mapp = map;

        abiltiesHud.SetUnit(unit.GetComponent<Unit>(), true);
        abiltiesHud.CurrentPlayer = player.GetComponent<Player>();
        storeController.setPlayer(unit.GetComponent<Unit>(), player.GetComponent<PlayerGameStats>());
        storeController.realStart();
    }

    void LoadAggressiveAI(int i)
    {
        GameObject player = Instantiate(AggressiveAIPrefab);
        player.transform.SetParent(playerParent);
        player.transform.name = "Player" + (i + 1);
        players.Add(player.GetComponent<Player>());
        player.GetComponent<AggressiveAIController>().Gamee = game;

        player.GetComponent<Player>().Color = colors[i];
        GameObject unit = Instantiate(unitPrefab);
        unit.transform.name = "Unit" + (i + 1);
        player.GetComponent<AIStoreController>().Target = unit.GetComponent<Unit>();
        player.GetComponent<AIStoreController>().TheGame = game;
        player.GetComponent<AIStoreController>().makeInstances();
        unit.AddComponent<AggressiveMovementUtility>();
        unit.GetComponent<AggressiveMovementUtility>().setup(unit.GetComponent<UnitMovement>(), map);
        unit.GetComponent<Unit>().Owner = player.GetComponent<Player>();
        player.GetComponent<AggressiveAIController>().units.Add(unit.GetComponent<Unit>());
        player.GetComponent<AggressiveAIController>().utilities.Add(unit.GetComponent<AggressiveMovementUtility>());
        units.Add(unit.GetComponent<Unit>());
        unit.GetComponent<UnitMovement>().Mapp = map;
        player.GetComponent<AggressiveAIController>().setup();
    }

    void LoadPlayMakerAI(int i)
    {

        GameObject player = Instantiate(PlaymakerAIPrefab);
        players.Add(player.GetComponent<Player>());
        player.transform.SetParent(playerParent);
        player.transform.name = "Player" + (i + 1);
        player.GetComponent<Player>().Color = colors[i];
        player.GetComponent<PlaymakerAIController>().Gamee = game;
        GameObject unit = Instantiate(unitPrefab);
        player.GetComponent<AIStoreController>().Target = unit.GetComponent<Unit>();
        player.GetComponent<AIStoreController>().TheGame = game;
        unit.transform.name = "Unit" + (i + 1);
        unit.AddComponent<PlaymakerMovementUtility>();
        unit.GetComponent<PlaymakerMovementUtility>().setup(unit.GetComponent<UnitMovement>(), map);
        player.GetComponent<AIStoreController>().Target = unit.GetComponent<Unit>();
        player.GetComponent<AIStoreController>().TheGame = game;
        player.GetComponent<AIStoreController>().makeInstances();
        unit.GetComponent<Unit>().Owner = player.GetComponent<Player>();
        player.GetComponent<PlaymakerAIController>().units.Add(unit.GetComponent<Unit>());
        player.GetComponent<PlaymakerAIController>().utilities.Add(unit.GetComponent<PlaymakerMovementUtility>());
        units.Add(unit.GetComponent<Unit>());
        unit.GetComponent<UnitMovement>().Mapp = map;
        player.GetComponent<PlaymakerAIController>().setup();
    }
    void LoadTankAI(int i)
    {
        GameObject player = Instantiate(TankAIPrefab);
        players.Add(player.GetComponent<Player>());
        player.transform.SetParent(playerParent);
        player.transform.name = "Player" + (i + 1);
        player.GetComponent<Player>().Color = colors[i];
        player.GetComponent<TankAIController>().Gamee = game;
        GameObject unit = Instantiate(unitPrefab);
        player.GetComponent<AIStoreController>().Target = unit.GetComponent<Unit>();
        player.GetComponent<AIStoreController>().TheGame = game;
        unit.transform.name = "Unit" + (i + 1);
        unit.AddComponent<TankAIMovementUtility>();
        unit.GetComponent<TankAIMovementUtility>().setup(unit.GetComponent<UnitMovement>(), map);
        player.GetComponent<AIStoreController>().Target = unit.GetComponent<Unit>();
        player.GetComponent<AIStoreController>().TheGame = game;
        player.GetComponent<AIStoreController>().makeInstances();
        unit.GetComponent<Unit>().Owner = player.GetComponent<Player>();
        player.GetComponent<TankAIController>().units.Add(unit.GetComponent<Unit>());
        player.GetComponent<TankAIController>().utilities.Add(unit.GetComponent<TankAIMovementUtility>());
        units.Add(unit.GetComponent<Unit>());
        unit.GetComponent<UnitMovement>().Mapp = map;
        player.GetComponent<TankAIController>().setup();
    }
}
