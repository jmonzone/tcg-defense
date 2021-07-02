using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Increase AOE")]
public class IncreaseAOE : Ability
{
    public float radius;
    public override void Activate(UnitView unit)
    {
        var unitAttacks = unit.GetComponent<IAOEHit>();
        unitAttacks.Radius += radius;
    }

    public override void Deactivate(UnitView unit)
    {
        var unitAttacks = unit.GetComponent<IAOEHit>();
        unitAttacks.Radius -= radius;
    }
}
