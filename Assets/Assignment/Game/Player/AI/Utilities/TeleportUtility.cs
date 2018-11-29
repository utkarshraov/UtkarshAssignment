using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportUtility : AbilityUtility {


    [SerializeField]
    private float castError;

    public override float calculateUtility()
    {
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else
        {
            utility = 2f;
        }

        return utility;
    }

    public override void performAction()
    {
        Vector3 location = utilityHelper.getTargetLocationWithOffset(caster.transform.position, 10);

        Ability ability = GetComponent<Ability>();
        PointTargeting pointTargeting = GetComponent<PointTargeting>();
        //pointTargeting.Bind(caster);
        if (pointTargeting != null)
        {
            pointTargeting.Target = utilityHelper.getTargetLocationWithOffset(location, castError);
        }
        string er = "";
        if (ability.CanCast(out er))
        {
            ability.Cast();
        }
    }
}
