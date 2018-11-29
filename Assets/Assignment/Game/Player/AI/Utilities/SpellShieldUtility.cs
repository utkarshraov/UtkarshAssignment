using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShieldUtility : AbilityUtility {



    public override float calculateUtility()
    {
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else
        {
            utility = 4f;
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
