using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using System;
public class Wave //Todo holds the data of the current battle
{

    public List<BaseMonster> Players = new List<BaseMonster>();
    public List<BaseMonster> Enemys = new List<BaseMonster>();
    public Move move;
    public BaseMonster m_Actor;
    public Wave() {
        Players.Add(new BaseMonster());
        Enemys.Add(new Tiger());
    }
    public bool IsPlayerDefeated() {
        return Players[0].m_Stats[StatTypes.HP] <= 0;
    }
    public bool IsEnemyDefeated() {
        return Enemys[0].m_Stats[StatTypes.HP] <= 0;
    }
}