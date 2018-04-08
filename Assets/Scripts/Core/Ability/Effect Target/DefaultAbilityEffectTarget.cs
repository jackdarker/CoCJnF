using UnityEngine;
using System.Collections;

public class DefaultAbilityEffectTarget : AbilityEffectTarget 
{
	public override bool IsTarget (BaseMonster tile)
	{
		if (tile == null)
			return false;

		Stats s = tile.m_Stats;
		return s != null && s[StatTypes.HP] > 0;
	}
}