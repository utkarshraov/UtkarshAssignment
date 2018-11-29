using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float distanceBetween(Unit One, Unit Two)
    {
        Vector3 delta = One.transform.position - Two.transform.position;

        return delta.magnitude;
    }

    public float distanceBetween(Vector3 location, Unit unit)
    {
        Vector3 delta = location - unit.transform.position;

        return delta.magnitude;
    }

    public List<Unit> getUnitsInArea(Vector3 targetLocation, float range, Unit caster)
    {
        Collider[] colls = Physics.OverlapSphere(targetLocation, range);
        List<Unit> targets = new List<Unit>();
        foreach (Collider coll in colls)
        {
            Unit target = coll.GetComponentInParent<Unit>();
            if (target != null && target != caster)
            {
                targets.Add(target);
            }
        }

        return targets;
    }

    public Unit getClosestUnit(Vector3 location, float maxRadius, Unit caster)
    {
        List<Unit> targets = getUnitsInArea(location, maxRadius, caster);
        Unit closestUnit = new Unit();
        if (targets.Count > 0)
        {
            float minDistance = 100;

            foreach (Unit unit in targets)
            {
                if(unit == caster)
                {
                    continue;
                }
                if (distanceBetween(location, unit) < minDistance)
                {
                    closestUnit = unit;
                }
            }
        }
        return closestUnit;
    }

    public Vector3 getTargetLocationWithOffset(Vector3 location, float range)
    {
        Vector2 random = Random.insideUnitCircle;
        Vector3 finalLocation = location + new Vector3(random.x, 0, random.y);
        return finalLocation;
    }
}
