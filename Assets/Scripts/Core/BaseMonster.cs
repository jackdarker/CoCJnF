using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base-class for all creatures
/// </summary>
public class BaseMonster : MonoBehaviour {
    public bool IsControlledByPlayer() {
        return false;
    }
    public readonly Stats m_Stats;

}

