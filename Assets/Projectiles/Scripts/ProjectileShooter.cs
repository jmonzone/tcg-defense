using System;
using UnityEngine;
using UnityEngine.Events;

// behaviour that shoots ProjectileViews from an ObjectPool
// the public Shoot() method should be called by a controller (ProjectileTower.cs, etc.)
public class ProjectileShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform shootTransform;
    [SerializeField] private ProjectileView projectilePrefab;

    [Header("Options")]
    [SerializeField] private float projectileSpeed = 10f;

    private ObjectPool<ProjectileView> projectilePool;

    public Vector3 ShootPosition => shootTransform.position;

    private void Start()
    {
        projectilePool = new ObjectPool<ProjectileView>(projectilePrefab);
    }

    public void Shoot(Vector3 direction, Action<Collision2D> onProjectileCollision)
    {
        var projectile = projectilePool.NextObject;
        projectile.Shoot(ShootPosition, direction.normalized * projectileSpeed, onProjectileCollision);
    }
}
