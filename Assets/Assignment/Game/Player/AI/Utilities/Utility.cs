using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour {

    [SerializeField]
    protected float utility;

    [SerializeField]
    public UtilityHelper utilityHelper = null;
    protected Unit caster;
    public Unit Caster { set { caster = value; } }

    public virtual float calculateUtility() { return utility; }

    public virtual void performAction() { }

}
