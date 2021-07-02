using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private LineRenderer path;
    [SerializeField] private EnemyView enemyPrefab;

    private float spawnCooldown = 1f;
    private Dictionary<Enemy, ObjectPool<EnemyView>> enemyPools = new Dictionary<Enemy, ObjectPool<EnemyView>>();

    private int enemiesRemaining;
    public int EnemiesRemaining
    {
        get => enemiesRemaining;
        private set
        {
            enemiesRemaining = value;
            if (enemiesRemaining <= 0)
            {
                enemiesRemaining = 0;
                OnNoEnemiesRemaining?.Invoke();
            }
        }
    }

    public event Action OnNoEnemiesRemaining;

    private void Start()
    {
        InitEnemyPool();
        roundManager.OnRoundStarted += StartSpawn;
    }

    private void InitEnemyPool()
    {

        var points = new List<Vector2>();
        for(var i = 0; i < path.positionCount; i++)
        {
            points.Add(path.GetPosition(i));
        }

        roundManager.Rounds.ForEach(round =>
        {
            round.Enemies.ForEach((enemy) =>
            {
                if (!enemyPools.ContainsKey(enemy.enemy))
                {
                    var pool = new ObjectPool<EnemyView>(enemyPrefab, 10);
                    pool.Pool.ForEach(enemyView =>
                    {
                        enemyView.Init(enemy.enemy);
                        enemyView.OnDeath += () => EnemiesRemaining--;
                        enemyView.GetComponent<EnemyMovement>().path = points;
                    });
                    enemyPools.Add(enemy.enemy, pool);
                }
            });
        });

    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        foreach(var enemyAmount in roundManager.CurrentRound.Enemies)
        {
            enemiesRemaining += enemyAmount.amount;
            for (var i = 0; i < enemyAmount.amount; i++)
            {
                yield return new WaitForSeconds(spawnCooldown);
                Spawn(enemyAmount.enemy);
            }
        }
    }

    private void Spawn(Enemy enemyType)
    {
        var enemy = enemyPools[enemyType].NextObject;
        enemy.Spawn();
    }
}
