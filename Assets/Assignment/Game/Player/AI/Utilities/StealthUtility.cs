using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthUtility : Utility {

    [SerializeField]
    private Cooldown cooldown;

    public override float calculateUtility()
    {
        if (!cooldown.Ready)
        {
            utility = 0f;
        }
        else if(!assessCooldowns())
        {
            utility = 3f;
        }
        else
        {
            utility = 0f;
        }

        return utility;
    }

    private bool assessCooldowns()
    {
        bool noSpells = false;
        foreach (Ability a in caster.Abilities)
        {
            if (!a.GetComponent<Cooldown>().Ready)
            {
                noSpells = true;
            }

        }
        return noSpells;
    }
}
