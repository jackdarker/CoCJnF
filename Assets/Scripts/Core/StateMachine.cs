using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StateMachine: MonoBehaviour
{
    [Serializable]
    public class State //: MonoBehaviour  //Todo why do we need monobehaviour
    {
        public string label;
        Action customEnter;
        Action customExit;
        public virtual void Enter()
        {
            AddListeners();
        }

        public virtual void Exit()
        {
            RemoveListeners();
        }

        protected virtual void OnDestroy()
        {
            RemoveListeners();
        }
        //override this to connect to Events
        protected virtual void AddListeners()
        {

        }
        //override this to disconnect from Events
        protected virtual void RemoveListeners()
        {

        }
        //called when abort button is pressed
        public virtual void OnCancle() {
        }
        //called when OK button is pressed
        public virtual void OnSubmit() {
        }
        public virtual void MoveRelative(Vector3 direction, Action didFinish) { }
            /*
            public State(Action enter, Action exit = null, string label = "")
            {
                customEnter = enter;
                customExit = exit;
                this.label = label;
            }

            public void Enter()
            {
                if (customEnter != null)
                    customEnter();
            }

            public void Exit()
            {
                if (customExit != null)
                    customExit();
            }*/
        }
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool _inTransition;
    /*public virtual T GetState<T>() where T : State        not required because no monobehaviour
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }*/
    public void ChangeState(State New) {
        CurrentState = New;
    }
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;

        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}