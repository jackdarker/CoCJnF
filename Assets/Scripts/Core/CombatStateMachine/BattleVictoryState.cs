using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called if the player won all Waves
public class BattleVictoryState : StateMachine.State {
    CombatStatemachine m_Owner;
    public BattleVictoryState(CombatStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        m_Owner.m_Info.ShowText("You won this battle", "Returning to Overworld...");
        m_Owner.m_Info.Show();
        
    }
    public override void OnSubmit() {
        Debug.Log("exiting combat");
        //Todo switch back to ExploreState - last Save-Position
    }
}

