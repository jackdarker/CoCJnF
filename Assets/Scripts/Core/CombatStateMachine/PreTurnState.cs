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
        m_Owner.m_SelButtons.SetBtList(null);
        m_Owner.m_Info.Hide();
        if (m_Owner.GetWave().IsPlayerDefeated()) { //if player defeated switch to Defeat state
            m_Owner.ChangeState(new BattleDefeatedState(m_Owner));
        } else if (m_Owner.GetWave().IsEnemyDefeated()) {  //if enemys defeated switch to Victory state
            m_Owner.ChangeState(new WaveVictoryState(m_Owner));
        } else {
            m_Owner.ChangeState(new PlayersTurnState(m_Owner));
        }
    }
}
