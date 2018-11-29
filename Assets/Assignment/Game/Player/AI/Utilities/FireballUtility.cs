using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballUtility : AbilityUtility {

   

    [SerializeField]
    private float hitRange;

    [SerializeField]
    private float castError;


    public override float calculateUtility()
    {
        hitRange = ( GetComponent<Level>().Current * 2 + 3);
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
            utility = 2f;
        }
        else
        {
            utility = 1f;
        }
        

        return utility;
    }

    public override void performAction()
    {
        Unit target = utilityHelper.getClosestUnit(caster.transform.position, hitRange, caster);
        Ability ability = GetComponent<Ability>();
        PointTargeting pointTargeting = GetComponent<PointTargeting>();
        //pointTargeting.Bind(caster);
        if(pointTargeting!=null)
        {
            pointTargeting.Target = utilityHelper.getTargetLocationWithOffset(target.transform.position, castError);
        }
        string er = "";
        if(ability.CanCast(out er))
        {
            ability.Cast();
        }
        
    }
}
