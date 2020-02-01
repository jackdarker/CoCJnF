using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using System;
public class Wave //Todo holds the data of the current battle
{

    public List<BaseActor> Players = new List<BaseActor>();
    public List<BaseActor> Enemys = new List<BaseActor>();
    public Move move;
    public BaseActor m_Actor;
    public Wave() {
        Players.Add(new BaseActor());  // Todo needs instantiate?
        Enemys.Add(new Tiger());
    }
    public bool IsPlayerDefeated() {
        return Players[0].m_Stats[StatTypes.HP] <= 0;
    }
    public bool IsEnemyDefeated() {
        return Enemys[0].m_Stats[StatTypes.HP] <= 0;
    }
}