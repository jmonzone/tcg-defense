using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PriorityType
{
    CLOSEST,
    FIRST,
    LAST,
    STRONGEST,
    WEAKEST,
}

public class UnitTargetSystem : UnitBehaviour
{
    [Header("Options")]
    [SerializeField] private PriorityType priority;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Transform model;

    public Transform Target { get; private set; }

    private void Update()
    {
        Target = GetTarget();
        if (Target)
        {
            model.up = transform.position - Target.position;
        }
    }

    private Transform GetTarget()
    {
        var targets = GetTargetsInRange();
        if (targets.Count == 0) return null;

        return GetClosestTarget(targets);
    }

    private List<Transform> GetTargetsInRange()
    {
        var hits = Physics2D.CircleCastAll(transform.position, Unit.Range, Vector2.zero, 0,targetLayers);
        var targets = new List<Transform>();
        hits.ToList().ForEach((x) => {
            if (x.collider.gameObject.activeSelf) targets.Add(x.collider.transform);
        });
        return targets;
    }

    private Transform GetClosestTarget(List<Transform> targets) => Utility.GetPriorityTarget(targets, (target) => Vector3.Distance(transform.position, target.position), false);

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Unit.Range);
    }
}
