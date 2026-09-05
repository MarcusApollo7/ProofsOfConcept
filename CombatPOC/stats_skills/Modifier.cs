namespace CombatPOC.stats_skills;

public enum StatModType
/*
Enum for the type of stat modifer

vals:
Flat: Add/sub from skill value
PercentAdd: Add/sub percentage value
PercentMult: Mult/div percentage value
*/
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatModifier
/*
Class for StatModifier
Used to organize modifiers for the stats

Parameters:
float Val: the value of the modifier
StatModType Type: the type of the modifier
int Order: the order in which the modifiers are applied, 
by default will use values from the StatModType so Flat + PercentAdd + PercentMult
object Source: the item the modifier comes from
*/
{
    // Parameters
    public readonly float Val;
    public readonly StatModType Type;
    public readonly int Order;
    public readonly object Source;

    public StatModifier(float value, StatModType type, int order, object source)
    {
        Val = value;
        Type = type;
        Order = order;
        Source = source;
    }

    // Constructors

    // Requires Value and Type. Calls the "Main" constructor and sets Order and Source to their default values: (int)type and null, respectively.
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    // Requires Value, Type and Order. Sets Source to its default value: null
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    // Requires Value, Type and Source. Sets Order to its default value: (int)Type
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}