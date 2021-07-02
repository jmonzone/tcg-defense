using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Increase Range")]
public class IncreaseRange : Ability
{
    public float rangeIncrease;

    public override void Activate(UnitView unit)
    {
        unit.Range += rangeIncrease;
    }

    public override void Deactivate(UnitView unit)
    {
        unit.Range -= rangeIncrease;
    }
}
