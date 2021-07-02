using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitItems : MonoBehaviour
{
    public Transform itemPosition;

    public List<Item> Items { get; private set; } = new List<Item>();

    public event Action<Item> OnItemEquipped;
    public event Action<Item> OnItemUnequipped;

    public void Equip(Item item)
    {
        Items.Add(item);
        OnItemEquipped?.Invoke(item);
    }

    public void UnequipItem(Item item)
    {
        Items.Remove(item);
        OnItemUnequipped?.Invoke(item);
    }
}
