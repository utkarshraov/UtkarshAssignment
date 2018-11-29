using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorUtility : AbilityUtility {

    [SerializeField]
    private float hitRange;

    [SerializeField]
    private float castError;

    public override float calculateUtility()
    {
        hitRange = GetComponent<Level>().Current *2;
        int nearbyUnits = utilityHelper.getUnitsInArea(transform.position, hitRange, caster).Count;
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else
        {
            utility = 2 * nearbyUnits;
        }


        return utility;
    }

    public override void performAction()
    {
        Vector3 target = utilityHelper.getClosestUnit(caster.transform.position, hitRange, caster).transform.position;
        Ability ability = GetComponent<Ability>();
        PointTargeting pointTargeting = GetComponent<PointTargeting>();
        if (pointTargeting != null)
        { 
            pointTargeting.Target = utilityHelper.getTargetLocationWithOffset(target, castError);
        }
        string er = "";
        if (ability.CanCast(out er))
        {
            ability.Cast();
        }
    }
}
