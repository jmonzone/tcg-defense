using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int MaxHealth { get; private set; }
    private int health;
    public int Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
            if (health == 0) Die();
            OnHealthChanged?.Invoke();
        }
    }

    private float speed = 1;
    public float Speed
    {
        get
        {
            var modifiedSpeed = speed;
            modifiers.ForEach(modifier => modifiedSpeed *= modifier.value);
            return modifiedSpeed;
        }
        private set => speed = value;
    }

    private List<Modifier> modifiers = new List<Modifier>();

    public List<Type> Statuses { get; private set; } = new List<Type>();


    public event Action OnHealthChanged;
    public event Action OnSpawn;
    public event Action OnDeath;


    public void Init(Enemy enemy)
    {
        Health = MaxHealth = enemy.Health;
        Speed = speed;
    }

    public void Spawn()
    {
        OnSpawn?.Invoke();
        gameObject.SetActive(true);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void Damage(int damage)
    {
        Health -= damage;
    }

    public void AddSpeedModifier(Modifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveSpeedModifier(Modifier modifier)
    {
        modifiers.Remove(modifier);
    }

    public void ApplyStatus(Status status)
    {
        if (!gameObject.activeSelf) return;
        if (Statuses.Contains(status.GetType())) return;
        Statuses.Add(status.GetType());
        if (status is Burn) Burn(status as Burn);
        if (status is Freeze) Freeze(status as Freeze);
    }

    public void RemoveStatus(Status status)
    {
        if (!Statuses.Contains(status.GetType())) return;
        Statuses.Remove(status.GetType());
    }

    private void Burn(Burn burn)
    {
        StartCoroutine(OverTimeEffect(burn.ticks, burn.duration, () => Damage(burn.damage), () => RemoveStatus(burn)));
    }

    private void Freeze(Freeze freeze)
    {
        var freezeModifier = new Modifier
        {
            name = "Frozen",
            value = 0
        };

        AddSpeedModifier(freezeModifier);

        StartCoroutine(DurationEffect(freeze.duration, () =>
        {
            RemoveSpeedModifier(freezeModifier);
            RemoveStatus(freeze);
        }));
    }


    public IEnumerator OverTimeEffect(int ticks, float duration, Action onTick, Action onComplete)
    {
        for (var i = 0; i < ticks; i++)
        {
            yield return new WaitForSeconds(duration / ticks);
            onTick?.Invoke();
        }
        onComplete?.Invoke();
    }

    public IEnumerator DurationEffect(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
