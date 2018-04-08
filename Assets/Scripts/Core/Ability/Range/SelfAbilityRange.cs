using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelfAbilityRange : AbilityRange 
{
	public override bool positionOriented { get { return false; }}

	public override List<BaseMonster> GetTilesInRange (Wave board)
	{
		List<BaseMonster> retValue = new List<BaseMonster>(1);
		//retValue.Add(unit.tile);
		return retValue;
	}
}