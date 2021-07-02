using System;
using UnityEngine;

public enum CardType
{
    UNIT,
    ITEM,
}

public abstract class Card : ScriptableObject
{
    public new string name;
    public Sprite sprite;

    public abstract void Select(Action onComplete, Action onCancel);
    public CardType Type
    {
        get
        {
            var type = GetType();
            if (type == typeof(Unit)) return CardType.UNIT;
            else if (type == typeof(Item)) return CardType.ITEM;

            Debug.LogError("Unknown Card Type");
            return CardType.UNIT;
        }
    }

    public Color Color
    {
        get
        {
            switch(Type)
            {
                case CardType.UNIT:
                    return Color.red;
                case CardType.ITEM:
                    return Color.green;
                default:
                    return Color.white;
            }
        }
    }
}
