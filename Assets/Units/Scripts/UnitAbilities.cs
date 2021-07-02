using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(UnitView))]
[RequireComponent(typeof(UnitItems))]
public class UnitAbilities : MonoBehaviour
{
    private UnitView unitView;
    private UnitItems unitItems;

    private Dictionary<Ability, int> abilityProgress = new Dictionary<Ability, int>();

    private void Start()
    {
        unitView = GetComponent<UnitView>();
        unitItems = GetComponent<UnitItems>();
        unitItems.OnItemEquipped += OnItemEquipped;
        unitItems.OnItemUnequipped += OnItemsUnequipped;
        RoundManager.Instance.OnRoundEnded += GainExperience;
    }

    private void GainExperience()
    {
        foreach(var item in unitItems.Items)
        {
            foreach(var ability in item.Abilities)
            {
                if (IsLearning(ability))
                {
                    abilityProgress[ability] = Mathf.Max(abilityProgress[ability], ability.points);
                }
            }
        }
    }

    private void OnItemEquipped(Item item)
    {
        item.Abilities.ForEach(ability =>
        {
            if (CanStartLearning(ability))
            {
                abilityProgress.Add(ability, 0);
            }

            if (IsLearning(ability))
            {
                ability.Activate(unitView);
            }
        });
    }

    private void OnItemsUnequipped(Item item)
    {
        item.Abilities.ForEach(ability =>
        {
            if (IsLearning(ability))
            {
                ability.Deactivate(unitView);
            }
        });
    }

    private bool CanStartLearning(Ability ability) => ability.units.Contains(unitView.Unit) && !abilityProgress.ContainsKey(ability);
    private bool IsLearning(Ability ability) => abilityProgress.ContainsKey(ability) && !HasLearned(ability);
    private bool HasLearned(Ability ability) => abilityProgress[ability] >= ability.points;
}
