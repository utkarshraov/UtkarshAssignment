using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameInformationUIController : MonoBehaviour,IAppAware {

    [SerializeField]
    private GameObject playerInformationPrefab = null;

    [SerializeField]
    private Transform ParentTransform;
    [SerializeField]
    private App app = null;
    public App App { get { return app; } set { app = value; } }

    private PlayerInfo[] playerInfos;

    [SerializeField]
    private GameInformation gameInformation = null;



    void Start () {
        playerInfos = new PlayerInfo[8];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onNumberChanged(int num)
    {
        Reset();
        gameInformation.NumberOfPlayers = num + 1;
        gameInformation.instancePCs(num + 1);
        for (int i=0;i<=num;i++)
        {
            GameObject PI = Instantiate(playerInformationPrefab);
            playerInfos[i] = PI.GetComponent<PlayerInfo>();
            playerInfos[i].setText("Player " + (i + 1));
            PI.transform.SetParent(ParentTransform);
        }

    }

    private void Reset()
    {
        gameInformation.resetPCs();
        for (int i =0;i<8;i++)
        {
            playerInfos[i] = null;
        }
        for(int i=0;i<ParentTransform.childCount;i++)
        {
            Destroy(ParentTransform.GetChild(i).gameObject);
        }
    }

    public void loadGame()
    {
        for (int i = 0; i < ParentTransform.childCount; i++)
        {
            Dropdown[] dropdowns = ParentTransform.GetChild(i).GetComponentsInChildren<Dropdown>();
            gameInformation.setPlayerController(i, dropdowns[0].value);
            gameInformation.setPLayerDifficulty(i, dropdowns[1].value);
        }

        if(app !=null)
        {
            app.Scenes.LoadGame();
        }
        else
        {
            SceneManager.LoadScene("WarlocksGame");
        }

    }
}
