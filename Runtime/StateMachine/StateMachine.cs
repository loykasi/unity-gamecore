namespace Gamecore.StateMachine
{
    public class StateMachine
    {
        private State _currentState;

        public void OnUpdate()
        {
            _currentState?.OnUpdate();
        }

        public void SetState(State _state)
        {
            _currentState?.OnExit();
            _currentState = _state;
            _currentState.OnEnter();
 
        }

        public void StopStateMachine()
        {
            _currentState?.OnExit();
            _currentState = null;
        }
    }
}