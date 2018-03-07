using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using System;
public class Battle //Todo holds the data of the current battle
{
    public class Combatant  //Todo collection of the creatures in battle for one player
    {
        public bool m_controlledByPlayer;
    }
    public List<Combatant> combatants = new List<Combatant>();
    public Move move;
    public int lastDamage;
    public Combatant attacker { get { return combatants[0]; } }
    public Combatant defender { get { return combatants[1]; } }
}