using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Increase Damage")]
public class IncreaseDamage : Ability
{
    public float damageIncrease;

    public override void Activate(UnitView unit)
    {
        unit.Damage += damageIncrease;
    }

    public override void Deactivate(UnitView unit)
    {
        unit.Damage -= damageIncrease;
    }
}
