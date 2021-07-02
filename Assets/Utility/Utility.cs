using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static T GetPriorityTarget<T>(List<T> targets, Func<T, float> getComparisonValue, bool getHighest = true)
    {
        var priorityTarget = targets[0];
        var valueToCompare = getComparisonValue(priorityTarget);

        targets.ForEach(target =>
        {
            var comparisonValue = getComparisonValue(target);
            if (getHighest && comparisonValue > valueToCompare || !getHighest && comparisonValue < valueToCompare)
            {
                valueToCompare = comparisonValue;
                priorityTarget = target;
            }
        });
        return priorityTarget;
    }
}
