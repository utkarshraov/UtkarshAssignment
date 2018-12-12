using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetworkPlayerLoader : MonoBehaviour {

    public static NetworkPlayerLoader instance = null;

    [SerializeField]
    public Game game = null;

    [SerializeField]
    public InGameUi inGameUi = null;

    [SerializeField]
    public AbilitiesHud abiltiesHud = null;

    [SerializeField]
    public InputManager inputManager = null;

    [SerializeField]
    public Map map = null;

    [SerializeField]
    public EventSystem eventsystem = null;

    [SerializeField]
    public GraphicRaycaster graphicRaycaster = null;

    [SerializeField]
    public Color[] colors = null;

    [SerializeField]
    public int numPlayers = 0;

    [SerializeField]
    public GameObject unitPrefab = null;

    [SerializeField]
    public GameObject localPlayerPrefab = null;

    [SerializeField]
    public StoreController storeController = null;

    [SerializeField]
    public Transform playerParent = null;

    public List<Player> players = new List<Player>();
    public List<Unit> units = new List<Unit>();


    private void Awake()
    {
        instance = this;
    }
}
