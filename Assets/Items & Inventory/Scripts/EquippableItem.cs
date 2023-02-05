using UnityEngine;

namespace Overgeared.CharacterStats.Examples
{
	public enum EquipmentType
	{
		Helmet,
		Chest,
		Gloves,
		Boots,
		Weapon1,
		Weapon2,
		Accessory1,
		Accessory2,
	}

	[CreateAssetMenu]
	public class EquippableItem : Item
	{
		public int StrengthBonus;
		public int AgilityBonus;
		public int IntelligenceBonus;
		public int VitalityBonus;
		[Space]
		public float StrengthPercentBonus;
		public float AgilityPercentBonus;
		public float IntelligencePercentBonus;
		public float VitalityPercentBonus;
		[Space]
		public EquipmentType EquipmentType;

		public void Equip(Character c)
		{
			if (StrengthBonus != 0)
				c.Strength.AddModifier(new CharacterStatModifier(StrengthBonus, StatModifierType.Flat, this));
			if (AgilityBonus != 0)
				c.Agility.AddModifier(new CharacterStatModifier(AgilityBonus, StatModifierType.Flat, this));
			if (IntelligenceBonus != 0)
				c.Intelligence.AddModifier(new CharacterStatModifier(IntelligenceBonus, StatModifierType.Flat, this));
			if (VitalityBonus != 0)
				c.Vitality.AddModifier(new CharacterStatModifier(VitalityBonus, StatModifierType.Flat, this));

			if (StrengthPercentBonus != 0)
				c.Strength.AddModifier(new CharacterStatModifier(StrengthPercentBonus, StatModifierType.PercentMultiply, this));
			if (AgilityPercentBonus != 0)
				c.Agility.AddModifier(new CharacterStatModifier(AgilityPercentBonus, StatModifierType.PercentMultiply, this));
			if (IntelligencePercentBonus != 0)
				c.Intelligence.AddModifier(new CharacterStatModifier(IntelligencePercentBonus, StatModifierType.PercentMultiply, this));
			if (VitalityPercentBonus != 0)
				c.Vitality.AddModifier(new CharacterStatModifier(VitalityPercentBonus, StatModifierType.PercentMultiply, this));
		}

		public void Unequip(Character c)
		{
			c.Strength.RemoveAllModifiersFromSource(this);
			c.Agility.RemoveAllModifiersFromSource(this);
			c.Intelligence.RemoveAllModifiersFromSource(this);
			c.Vitality.RemoveAllModifiersFromSource(this);
		}
	}
}