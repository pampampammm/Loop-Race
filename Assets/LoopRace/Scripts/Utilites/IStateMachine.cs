public interface IStateMachine<T>
{
    T Current { get; }
    void SetState<TState>() where TState : T;
}