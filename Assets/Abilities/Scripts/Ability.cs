using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;
    public int points = 100;
    public List<Unit> units;

    public abstract void Activate(UnitView unit);
    public abstract void Deactivate(UnitView unit);
}
