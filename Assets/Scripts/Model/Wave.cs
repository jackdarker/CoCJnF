using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using System;
public class Wave //Todo holds the data of the current battle
{

    public List<BaseMonster> combatants = new List<BaseMonster>();
    public Move move;
    public BaseMonster m_Actor;
}