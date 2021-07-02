using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int health;

    public Sprite Sprite => sprite;
    public int Health => health;
}
