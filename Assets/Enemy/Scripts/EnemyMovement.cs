using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    protected EnemyView enemy;
    protected virtual void Awake()
    {
        enemy = GetComponent<EnemyView>();
        enemy.OnSpawn += OnSpawn;
    }

    protected virtual void OnSpawn() { }
}

public class EnemyMovement : EnemyComponent
{
    public List<Vector2> path;
    private int index = 0;

    protected override void OnSpawn()
    {
        base.OnSpawn();
        index = 0;
        transform.position = path[index];
    }

    private void Update()
    {
        var destination = path[index + 1];

        if (Vector2.Distance(destination, transform.position) > 0.005f)
        {
            var direction = (destination - (Vector2)transform.position).normalized;
            transform.Translate(direction * 0.001f * enemy.Speed);
        }
        else
        {
            index++;
            if (index == path.Count - 1) enemy.Die();
        }
    }


}
