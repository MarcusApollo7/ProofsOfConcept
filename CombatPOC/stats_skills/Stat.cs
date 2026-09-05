using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CombatPOC.stats_skills;

public class Stat
/*
Class for the Stat 
Handles computing the of the value checked by SkillChecker (_value) with the various StatModTypes
as well as removing 

Parameters:
bool isDirty: initially set to true, tracks if the _value needs to be recomputed
float BaseVal: Base value of the stat
float _value: the internal structure of the value with all modifiers
float lastBaseVal: used in case the _value needs to be computed
float Value: used to get the value of the stat with all modifiers

Methods:
AddModifier/RemoveModifier: Adds/removes modifier and sets isDirty to true, sorts modifiers by order if needed
ComputeValue: Computes the value of the stat with all modifiers by iterating
over the List<StatModifier> and performing the appropriate calculations
CompareModifierOrder: used in AddModifier as function passed into Sort
RemoveAllModifiersFromSource: Removes all modifiers with a given source
*/
{
    public string name;
    protected bool isDirty = true;
    public float BaseVal;
    protected float _value;
    protected float lastBaseVal = float.MinValue;
    private object value;

    public virtual float Value
    {
        get
        {
            if (isDirty || lastBaseVal != BaseVal)
            {
                lastBaseVal = BaseVal;
                _value = ComputeValue();
                isDirty = false;
            }
            return _value;
        }
    }

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public Stat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public Stat(float baseVal, string Name) : this()
    {
        BaseVal = baseVal;
        name = Name;
    }

    public Stat(object value)
    {
        this.value = value;
    }



    // Methods

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    protected virtual float ComputeValue()
    {
        float finalVal = BaseVal;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat)
            {
                finalVal += mod.Val;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Val; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalVal *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalVal *= 1 + mod.Val;
            }
        }
        return (float)Math.Round(finalVal, 4);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        else
            return 0;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

}

