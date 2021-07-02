using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Round")]
public class Round : ScriptableObject
{
    [System.Serializable]
    public struct EnemyAmount
    {
        public Enemy enemy;
        public int amount;
    }

    public List<EnemyAmount> Enemies;
}
