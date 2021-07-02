using System.Collections.Generic;
using UnityEngine;

// script to pool game objects  (projectiles, ally objects, enemies)
// todo: add a GetNextAvailableObject method
// todo: add a resize flag to add more objects to pool, if there are no availble objects left

public class ObjectPool<T> where T : MonoBehaviour
{
    public List<T> Pool { get; private set;} = new List<T>();
    private int poolIndex = -1;

    public T NextObject
    {
        get
        {
            poolIndex = (poolIndex + 1) % Pool.Count;
            return Pool[poolIndex];
        }
    }

    public ObjectPool(T prefab, int count = 5)
    {
        Pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            obj.name = $"{typeof(T)} {i}";
            Pool.Add(obj);
        }
    }
}
