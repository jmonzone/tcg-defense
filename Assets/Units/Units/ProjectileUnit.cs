using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IAOEHit
{
    float Radius { get; set; }
}

public class ProjectileUnit : UnitBehaviour, IAOEHit
{
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private UnitTargetSystem unitTargeter;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private AOEView aoePrefab;

    private AOEView aoe;

    public float Radius { get; set; } = 0;

    private float AttackCooldown => 1 / Unit.AttackSpeed;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackCorutine());
        aoe = Instantiate(aoePrefab);
    }

    private IEnumerator AttackCorutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(AttackCooldown);
            if (unitTargeter.Target) Shoot(unitTargeter.Target);
        }
        
    }

    private void Shoot(Transform target)
    {
        var direction = target.position - projectileShooter.ShootPosition;
        projectileShooter.Shoot(direction.normalized, (col) =>
        {
            Action<Transform> hitEnemy = transform =>
            {
                var enemy = transform.GetComponentInParent<EnemyView>();
                if (enemy) Unit.OnHit?.Invoke(enemy);
            };

            if (Radius > 0)
            {
                var hits = Physics2D.CircleCastAll(col.transform.position, Radius, Vector2.zero, 0, targetLayer);
                hits.ToList().ForEach((hit) => hitEnemy(hit.transform));

                aoe.Spawn(col.transform.position, Radius);
            }
            else hitEnemy(col.transform);
           
        });
    }
}
