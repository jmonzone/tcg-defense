using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Unit")]
public class Unit : Card
{
    public GameObject prefab;

    public float baseRange = 1.5f;
    public float baseDamage = 1;
    public float baseAttackSpeed = 1;

    public override void Select(Action onComplete, Action onCancel)
    {
        var unit = Instantiate(prefab).GetComponent<UnitView>();
        unit.Init(this);

        var placement = unit.GetComponent<PlaceableObject>();
        placement.StartPlacement(onComplete: _ =>
        {
            onComplete?.Invoke();
        }, 
        onCancel: () =>
        {
            onCancel?.Invoke();
            Destroy(unit.gameObject);
        });
    }
}
