using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAIController : PlayerController {

    private State currentState = State.Shopping;

    //danger value to determine how and when the AI should attack
    private float danger = 0f;

    [SerializeField]
    private float dangerThreshold;

    private float maxRange;

    private Unit unit;

	protected override void Start () {
        unit = Units[0] as Unit;
        base.Start();
        StartCoroutine(Act());
	}
	
	// Update is called once per frame
	void Update () {
		
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

            if(danger<dangerThreshold)
            {
                currentState = State.Aggressive;
            }


            yield return new WaitForSeconds(1f);
        }
    }

    private void assessCooldowns()
    {
        foreach(Ability a in unit.Abilities)
        {
            
        }
    }

    private void getNearbyUnits()
    {

    }
}
