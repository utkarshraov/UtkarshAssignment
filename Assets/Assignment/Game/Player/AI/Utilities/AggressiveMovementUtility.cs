using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveMovementUtility : MovementUtility {

    public override float calculateUtility()
    {
        utilityHelper = GetComponent<UtilityHelper>();
        if (utilityHelper.getUnitsInArea(transform.position,8, caster).Count > 1)
        {
            utility = 1;
        }
        else
        {
            utility = 3;
        }

        return utility;
    }

    public override void performAction()
    {
        bool validLocation = false;
        int numAttempts = 0;
        Vector3 targetLocation;
        do
        {
            targetLocation = caster.transform.position + Random.insideUnitSphere * 10;
            Vector3 normal;
            map.GetMapPointFromWorldPoint(targetLocation, out targetLocation, out normal);
            validLocation = true;

            if ((targetLocation - Vector3.zero).magnitude > map.GetComponentInChildren<Island>().Radius)
            {
                validLocation = false;
            }

            numAttempts++;
        } while (!validLocation && numAttempts < 10);
        unitMovement.MoveTo(targetLocation);
    }
}
