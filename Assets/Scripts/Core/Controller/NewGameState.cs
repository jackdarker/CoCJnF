using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlowController : MonoBehaviour
{
    /*
    StateMachine.State NewGameState
    {
        get
        {
            if (_NewGameState == null)
                _NewGameState = new StateMachine.State(OnEnterPlayerConfigureState, OnExitPlayerConfigureState, "Player Configure");
            return _NewGameState;
        }
    }
    StateMachine.State _NewGameState;

    void OnEnterPlayerConfigureState()
    {
        game = GameFactory.Create(1);
        playerConfigureViewController.gameObject.SetActive(true);
        playerConfigureViewController.didComplete = delegate {
            stateMachine.ChangeState(ExploreState);
        };
       /* playerConfigureViewController.didAbort = delegate {   //Todo back to mainmenu
            stateMachine.ChangeState(PlayerCountState);
        };*/
   /* }

    void OnExitPlayerConfigureState()
    {
        playerConfigureViewController.didComplete = null;
        playerConfigureViewController.didAbort = null;
        playerConfigureViewController.gameObject.SetActive(false);
    }*/
}