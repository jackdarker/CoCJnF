using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base-class for all creatures
/// </summary>
public class BaseMonster : MonoBehaviour {
    public BaseMonster() {

        this.m_Stats.SetValue(StatTypes.HP, 20, false);
    }
    public bool IsControlledByPlayer() {
        return false;
    }
    public readonly Stats m_Stats = new Stats();

}

