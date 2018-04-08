using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAbilityArea : AbilityArea 
{
	public override List<BaseMonster> GetTilesInArea (Wave board, Vector3 pos)
	{
		List<BaseMonster> retValue = new List<BaseMonster>();
        return retValue;
        /*
		Tile tile = board.GetTile(pos);
		if (tile != null)
			retValue.Add(tile);
		return retValue;*/
	}
}
