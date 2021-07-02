using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Apply Status")]
public class ApplyStatus : Ability
{
    public Status status;

    public override void Activate(UnitView unit)
    {
        unit.OnHit += Apply;
    }

    public override void Deactivate(UnitView unit)
    {
        unit.OnHit -= Apply;
    }

    private void Apply(EnemyView enemy)
    {
        enemy.ApplyStatus(status);
    }
}

