using UnityEngine;

public partial class FlowController : MonoBehaviour
{

    StateMachine.State MenuState
    {
        get
        {
            if (_menuState == null)
                _menuState = new StateMachine.State(OnEnterMenuState, OnExitMenuState, "Menu");
            return _menuState;
        }
    }
    StateMachine.State _menuState;

    void OnEnterMenuState()
    {
        introMenuViewController.gameObject.SetActive(true);
        introMenuViewController.didFinish = delegate (IntroMenuViewController.Exits obj) {
            switch (obj)
            {
                case IntroMenuViewController.Exits.New:
                    stateMachine.ChangeState(NewGameState);
                    break;
                case IntroMenuViewController.Exits.Load:
                    stateMachine.ChangeState(NewGameState);  //??Load
                    break;
            }
        };
    }

    void OnExitMenuState()
    {
        introMenuViewController.didFinish = null;
        introMenuViewController.gameObject.SetActive(false);
    }
}