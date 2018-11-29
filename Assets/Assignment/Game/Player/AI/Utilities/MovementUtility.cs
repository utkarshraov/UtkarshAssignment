using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtility : Utility {

    [SerializeField]
    protected UnitMovement unitMovement = null;

    [SerializeField]
    protected Map map;


    public void setup(UnitMovement um, Map mapp) { unitMovement = um; map = mapp; }
}
