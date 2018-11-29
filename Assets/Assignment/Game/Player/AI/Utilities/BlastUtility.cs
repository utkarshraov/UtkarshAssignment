using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastUtility : AbilityUtility {

    [SerializeField]
    private float hitRange;

    [SerializeField]
    private float castError;
    public override float calculateUtility()
    {
        hitRange = (GetComponent<Level>().Current * 2 + 1);
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else if(utilityHelper.getUnitsInArea(transform.position, hitRange, caster).Count < 1)
        {
            utility = 0;
        }
        else if(utilityHelper.getUnitsInArea(transform.position, hitRange, caster).Count > 2)
        {
            utility = 4;
        }

        return utility;
    }

    public override void performAction()
    {
        Ability ability = GetComponent<Ability>();
        string er = "";
        if (ability.CanCast(out er))
        {
            ability.Cast();
        }
    }
}
