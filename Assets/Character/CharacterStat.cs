using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{
    public float BaseValue;

    protected bool m_bIsDirty = true;
    protected float m_fLastBaseValue;

    protected float m_fValue;
    public virtual float Value
    {
        get
        {
            if (m_bIsDirty || m_fLastBaseValue != BaseValue)
            {
                m_fLastBaseValue = BaseValue;
                m_fValue = CalculateFinalValue();
                m_bIsDirty = false;
            }

            return m_fValue;
        }
    }

    protected readonly List<CharacterStatModifier> m_statModifiers;
    public readonly ReadOnlyCollection<CharacterStatModifier> StatModifiers;

    public CharacterStat()
    {
        m_statModifiers = new List<CharacterStatModifier>();
        StatModifiers = m_statModifiers.AsReadOnly();
    }

    public CharacterStat(float m_fBaseValue): this()
    {
        BaseValue = m_fBaseValue;
    }

    public virtual void AddModifier(CharacterStatModifier pModifier)
    {
        m_bIsDirty = true;
        m_statModifiers.Add(pModifier);
    }

    public virtual bool RemoveModifier(CharacterStatModifier pModifier)
    {
        if (m_statModifiers.Remove(pModifier))
        {
            m_bIsDirty = true;
            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object pSource)
    {
        int iNumRemovals = m_statModifiers.RemoveAll(pModifier => pModifier.Source == pSource);

        if (iNumRemovals > 0)
        {
            m_bIsDirty = true;
            return true;
        }

        return false;
    }

    protected virtual int CompareModifierOrder(CharacterStatModifier pLeft, CharacterStatModifier pRight)
    {
        if (pLeft.Order < pRight.Order)
        {
            return -1;
        }
        else if (pLeft.Order > pRight.Order)
        {
            return 1;
        }

        return 0;
    }

    protected virtual float CalculateFinalValue()
    {
        float fFinalValue = BaseValue;
        float fSumPercentAdd = 0;

        m_statModifiers.Sort(CompareModifierOrder);

        for (int iIndex = 0; iIndex < m_statModifiers.Count; iIndex++)
        {
            CharacterStatModifier pModifier = m_statModifiers[iIndex];

            if (pModifier.Type == StatModifierType.Flat)
            {
                fFinalValue += pModifier.Value;
            }
            else if (pModifier.Type == StatModifierType.PercentAdd)
            {
                fSumPercentAdd += pModifier.Value;

                if (iIndex + 1 >= m_statModifiers.Count || m_statModifiers[iIndex + 1].Type != StatModifierType.PercentAdd)
                {
                    fFinalValue *= 1 + fSumPercentAdd;
                    fSumPercentAdd = 0;
                }
            }
            else if (pModifier.Type == StatModifierType.PercentMultiply)
            {
                fFinalValue *= 1 + pModifier.Value;
            }
        }

        return (float)Math.Round(fFinalValue, 4);
    }
}
