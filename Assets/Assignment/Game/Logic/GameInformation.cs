using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInformation : MonoBehaviour{

    [SerializeField]
    private int numberOfPlayers;

    public int NumberOfPlayers { get { return numberOfPlayers; } set { numberOfPlayers = value; } }
    
    [SerializeField]
    private int[] playerControllers;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setPlayerController(int index, int pcIndex)
    {
        playerControllers[index] = pcIndex;
    }
}
