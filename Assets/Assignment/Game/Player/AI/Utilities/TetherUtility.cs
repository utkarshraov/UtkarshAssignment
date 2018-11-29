using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherUtility : AbilityUtility {

    [SerializeField]
    private float hitRange;

    [SerializeField]
    private float castError;
    public override float calculateUtility()
    { 

    Unit closest = utilityHelper.getClosestUnit(transform.position, hitRange, caster);
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else if(closest == null)
        {
            utility = 0f;
        }
        else if(utilityHelper.distanceBetween(transform.position, closest)< hitRange)
        {
            if(utilityHelper.getClosestUnit(closest.transform.position,4, closest) !=null)
            {
                utility = 5f;
            }
            else
            {
                utility = 3f;
            }
        }

        return utility;
    }
    public override void performAction()
    {
        Unit target = utilityHelper.getClosestUnit(caster.transform.position, hitRange, caster);
        Ability ability = GetComponent<Ability>();
        PointTargeting pointTargeting = GetComponent<PointTargeting>();
        //pointTargeting.Bind(caster);
        if (pointTargeting != null)
        {
            pointTargeting.Target = utilityHelper.getTargetLocationWithOffset(target.transform.position, castError);
        }
        string er = "";
        if (ability.CanCast(out er))
        {
            ability.Cast();
        }

    }
}
