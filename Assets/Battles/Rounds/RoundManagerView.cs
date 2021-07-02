using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManagerView : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(() => roundManager.StartRound());
        roundManager.OnRoundStarted += OnRoundStarted;
        roundManager.OnRoundEnded += OnRoundEnded;
    }

    private void OnRoundStarted()
    {
        button.gameObject.SetActive(false);
    }

    private void OnRoundEnded()
    {
        button.gameObject.SetActive(true);
    }
}
