using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityArea : MonoBehaviour
{
	public abstract List<BaseMonster> GetTilesInArea (Wave board, Vector3 pos);
}