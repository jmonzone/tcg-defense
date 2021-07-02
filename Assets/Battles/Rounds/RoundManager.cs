using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private List<Round> rounds;

    public static RoundManager Instance { get; private set; }
    private int roundIndex = 0;

    public Round CurrentRound => Rounds[roundIndex];
    public List<Round> Rounds => rounds;

    public event Action OnRoundStarted;
    public event Action OnRoundEnded;

    private void Awake()
    {
        enemyManager.OnNoEnemiesRemaining += EndRound;

        Instance = this;
    }

    public void StartRound()
    {
        if (roundIndex < rounds.Count) OnRoundStarted?.Invoke();
    }

    private void EndRound()
    {
        roundIndex++;

        OnRoundEnded?.Invoke();

        if (roundIndex >= rounds.Count) Debug.Log("Game Complete!");
    }
}
