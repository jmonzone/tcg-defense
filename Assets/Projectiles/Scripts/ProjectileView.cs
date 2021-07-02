using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    private Rigidbody2D rb;
    private Action<Collision2D> cachedOnCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector3 startPosition, Vector3 force, Action<Collision2D> onCollision)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        cachedOnCollision = onCollision;


        rb.velocity = force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        cachedOnCollision?.Invoke(collision);
        gameObject.SetActive(false);
    }
}
