﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIController : PlayerController {


    //danger value to determine how and when the AI should attack
    private float danger = 0f;

    public List<Utility> utilities;

    [SerializeField]
    private float dangerThreshold;

    [SerializeField]
    private float reactionTime = 2f;
    public float ReactionTime { set { reactionTime = value; } }

    [SerializeField]
    AIStoreController aIStoreController = null;

    private float maxRange;

    private Unit unit;

    protected override void Start()
    {
        unit = Units[0] as Unit;
        base.Start();
        StartCoroutine(Act());
    }

    public void setup()
    {
        aIStoreController.PurchaseAbility(0);
        aIStoreController.PurchaseAbility(1);
        aIStoreController.PurchaseItem(0);
        aIStoreController.PurchaseItem(1);
        aIStoreController.PurchaseAbility(0);
        aIStoreController.PurchaseAbility(1);
        aIStoreController.PurchaseItem(0);
        aIStoreController.PurchaseItem(1);
        aIStoreController.PurchaseAbility(0);
        aIStoreController.PurchaseAbility(1);
        aIStoreController.PurchaseItem(0);
        aIStoreController.PurchaseItem(1);
    }

    IEnumerator Act()
    {
        danger = 0;
        unit = Units[0] as Unit;
        while (true)
        {
            if (unit == null || unit.Health.IsDead)
            {
                yield return null;
                continue;
            }
            utilities = new List<Utility>(unit.GetComponentsInChildren<Utility>());
            foreach (Utility utility in utilities)
            {
                utility.utilityHelper = unit.GetComponent<UtilityHelper>();
                utility.Caster = unit;
            }
            float maxUtility = 0;
            int index = 0;

            for (int i = 0; i < utilities.Count; i++)
            {
                float util = utilities[i].calculateUtility();
                if (util > maxUtility)
                {
                    maxUtility = util;
                    index = i;
                }
            }
            if (maxUtility > 0)
            {
                utilities[index].performAction();
            }


            yield return new WaitForSeconds(reactionTime);
        }
    }
}
