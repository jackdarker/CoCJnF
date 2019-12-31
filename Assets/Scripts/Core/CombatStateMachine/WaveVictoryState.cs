using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called after Player defeated all enemys of the Wave
public class WaveVictoryState : StateMachine.State {

    CombatStatemachine m_Owner;
    public WaveVictoryState(CombatStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        m_Owner.ChangeState(new NextWaveState(m_Owner));
    }
}
