using UnityEngine;
using System.Collections;

public enum StatModifierType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMultiply = 200
}

public class CharacterStatModifier
{
    public readonly float Value;
    public readonly StatModifierType Type;
    public readonly int Order;
    public readonly object Source;

    public CharacterStatModifier(float fValue, StatModifierType eType, int iOrder, object pSource)
    {
        Value = fValue;
        Type = eType;
        Order = iOrder;
        Source = pSource;
    }

    public CharacterStatModifier(float fValue, StatModifierType eType) : this(fValue, eType, (int)eType, null) { }

    public CharacterStatModifier(float fValue, StatModifierType eType, int iOrder) : this(fValue, eType, iOrder, null) { }

    public CharacterStatModifier(float fValue, StatModifierType eType, object pSource) : this(fValue, eType, (int)eType, pSource) { }
}
