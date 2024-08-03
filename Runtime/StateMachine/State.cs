namespace Gamecore.StateMachine
{
    public abstract class State
    {
        public StateMachine StateMachine;
        
        public State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}