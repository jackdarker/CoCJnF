using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Called at start of wave/ after all combatants acted
//calculate turn order
public class PreTurnState : StateMachine.State {

    CombatStatemachine m_Owner;
    public PreTurnState(CombatStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        //if player defeted switch to Defeat state
        //if enemys defeated switch to Victory state
        if (false) {
        } else m_Owner.ChangeState(new PlayersTurnState(m_Owner));
    }
}
