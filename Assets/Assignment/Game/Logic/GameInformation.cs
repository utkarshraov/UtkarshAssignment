using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInformation : MonoBehaviour{

    [SerializeField]
    private int numberOfPlayers;

    public int NumberOfPlayers { get { return numberOfPlayers; } set { numberOfPlayers = value; } }
    
    [SerializeField]
    private int[] playerControllers;

    [SerializeField]
    private int[] difficulty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setPlayerController(int index, int pcIndex)
    {
        playerControllers[index] = pcIndex;
    }
    public void setPLayerDifficulty(int index, int dIndex)
    {
        difficulty[index] = dIndex;
    }

    public int getPlayerController(int index)
    {
        return playerControllers[index];
    }
    public int returnDifficulty(int index)
    {
        return difficulty[index];
    }

    public void instancePCs(int num)
    {
        playerControllers = new int[num];
        difficulty = new int[num];
    }

    public void resetPCs()
    {
        playerControllers = null;
        difficulty = null;
    }
}
