using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Item Item { get; private set; }

    public void Init(Item item)
    {
        Item = item;
        spriteRenderer.sprite = item.sprite;
    }
}
