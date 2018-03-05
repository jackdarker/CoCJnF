using System;



[Serializable]
public class StateMachine
{
    [Serializable]
    public class State
    {
        public string label;
        Action customEnter;
        Action customExit;

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
        }
    }
public State current;

    public void ChangeState(State target)
    {
        if (current == target)
            return;

        if (current != null)
            current.Exit();

        current = target;

        if (current != null)
            current.Enter();
    }
}