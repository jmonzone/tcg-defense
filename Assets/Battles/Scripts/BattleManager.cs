using System;
using UnityEngine;

public enum ControlState
{
    IDLE,
    EQUIPPING_ITEM,
    PLACING_UNIT
}

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
