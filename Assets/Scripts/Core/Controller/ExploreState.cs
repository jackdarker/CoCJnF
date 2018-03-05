using UnityEngine;


public partial class FlowController : MonoBehaviour
{
    StateMachine.State ExploreState
    {
        get
        {
            if (_ExploreState == null)
                _ExploreState = new StateMachine.State(OnEnterExploreState, OnExitExploreState, "Intro");
            return _ExploreState;
        }
    }
    StateMachine.State _ExploreState;

    void OnEnterExploreState()
    {
        //??musicController.PlayIntro();
        //??introLogoViewController.gameObject.SetActive(true);
        exploreViewController.gameObject.SetActive(true);
       /* exploreViewController.didFinish = delegate {  //Todo where to go
            stateMachine.ChangeState(SetupState);
        };*/
    }

    void OnExitExploreState()
    {
        exploreViewController.didFinish = null;
        exploreViewController.gameObject.SetActive(false);
    }
}