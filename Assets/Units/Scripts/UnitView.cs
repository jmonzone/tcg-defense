using System;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    private float range = 2.0f;

    public Unit Unit { get; private set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; private set; }
    public float Range
    {
        get => range;
        set
        {
            range = value;
            OnRangeChanged?.Invoke();
        }
    }

    public Action<EnemyView> OnHit;

    public event Action OnRangeChanged;
    
    public void Init(Unit unit)
    {
        Unit = unit;
        Damage = unit.baseDamage;
        AttackSpeed = unit.baseAttackSpeed;
        Range = unit.baseRange;

        OnHit += (enemy) => enemy.Damage(Mathf.CeilToInt(Damage));
    }
}
