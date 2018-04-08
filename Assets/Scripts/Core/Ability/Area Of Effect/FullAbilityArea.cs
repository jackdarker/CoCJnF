using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FullAbilityArea : AbilityArea 
{
	public override List<BaseMonster> GetTilesInArea (Wave board, Vector3 pos)
	{
		AbilityRange ar = GetComponent<AbilityRange>();
		return ar.GetTilesInRange(board);
	}
}