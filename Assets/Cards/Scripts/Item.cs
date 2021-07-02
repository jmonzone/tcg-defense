using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Item")]
public class Item : Card
{
    public List<Ability> Abilities;

    public override void Select(Action onComplete, Action onCancel)
    {
        var item = Instantiate(Resources.Load<GameObject>("Prefabs/Item")).GetComponent<ItemView>();
        item.Init(this);

        void Cancel()
        {
            onCancel?.Invoke();
            Destroy(item.gameObject);
        }

        void Complete(RaycastHit2D hit)
        {
            var unit = hit.transform.GetComponentInParent<UnitItems>();
            if (unit.Items.Count < 1)
            {
                unit.Equip(this);
                Place(hit);
                onComplete?.Invoke();
            }
            else
            {
                Cancel();
                Debug.Log($"Item.cs: {unit.GetComponent<UnitView>().Unit.name} Unit can not equip any more items.");
            }
        }

        void Place(RaycastHit2D hit)
        {
            var unit = hit.transform.GetComponentInParent<UnitItems>();
            item.transform.position = unit.itemPosition.position;
        }

        var placement = item.GetComponent<PlaceableObject>();
        placement.StartPlacement(Complete, Cancel, Place);
    }

}
