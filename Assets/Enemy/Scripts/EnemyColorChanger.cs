using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChanger : EnemyComponent
{
    private SpriteRenderer renderer;
    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        enemy.OnHealthChanged += UpdateColor;
        UpdateColor();
    }

    private void UpdateColor()
    {
        renderer.color = Color.Lerp(Color.red, Color.green, enemy.Health / (float)enemy.MaxHealth);
    }
}
