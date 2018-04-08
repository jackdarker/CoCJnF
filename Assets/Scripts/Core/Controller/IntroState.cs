using UnityEngine;


public partial class FlowController : MonoBehaviour
{
    /*StateMachine.State IntroState
    {
        get
        {
            if (_introState == null)
                _introState = new StateMachine.State(OnEnterIntroState, OnExitIntroState, "Intro");
            return _introState;
        }
    }
    StateMachine.State _introState;

    void OnEnterIntroState()
    {
        //??musicController.PlayIntro();
        //??introLogoViewController.gameObject.SetActive(true);
        introMenuViewController.gameObject.SetActive(true);
        introMenuViewController.didFinish = delegate {
            stateMachine.ChangeState(MenuState);
        };
    }

    void OnExitIntroState()
    {
        introMenuViewController.didFinish = null;
        introMenuViewController.gameObject.SetActive(false);
    }*/
}