using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStoreController : MonoBehaviour {

    [SerializeField] private Transform abilityContent = null;
    [SerializeField] private Transform itemContent = null;

    [SerializeField] private List<Purchasable> abilities = new List<Purchasable>();
    [SerializeField] private List<Purchasable> items = new List<Purchasable>();


    [SerializeField] private PlayerGameStats playerStats = null;
    [SerializeField] private Unit target = null;
    public Unit Target { set { target = value; } }
    [SerializeField] private Game game = null;
    public Game TheGame { set { game = value; } }


    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void makeInstances()
    {
        List<Purchasable> mainPrefabs = new List<Purchasable>(abilities);
        abilities.Clear();
        foreach (Purchasable purchasablePrefab in mainPrefabs)
        {
            Purchasable purchasable = Instantiate<Purchasable>(purchasablePrefab);
            purchasable.Target = target;
            purchasable.Game = game;
            abilities.Add(purchasable);
        }

        List<Purchasable> sidePrefabs = new List<Purchasable>(items);
        items.Clear();
        foreach (Purchasable purchasablePrefab in sidePrefabs)
        {
            Purchasable purchasable = Instantiate<Purchasable>(purchasablePrefab);
            purchasable.Target = target;
            purchasable.Game = game;
            items.Add(purchasable);
        }
    }

    public void PurchaseAbility(int index)
    {
        Purchasable purchasable = abilities[index];
        int cost = purchasable.Cost;
        if (cost <= playerStats.Gold)
        {
            playerStats.Gold -= cost;
            purchasable.OnPurchased(target);
        }
    }
    public void PurchaseItem(int index)
    {
        Purchasable purchasable = items[index];
        int cost = purchasable.Cost;
        if (cost <= playerStats.Gold)
        {
            playerStats.Gold -= cost;
            purchasable.OnPurchased();
        }
    }
}
