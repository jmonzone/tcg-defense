using System;
using System.Collections;
using UnityEngine;

public enum StatusType
{
    NULL,
    BURN,
    FREEZE
}
public abstract class Status : ScriptableObject
{
    public float duration;
}

[CreateAssetMenu(menuName = "Scriptable Objects/Status/Burn")]
public class Burn : Status
{
    public int damage;
    public int ticks;
}
