using UnityEngine;


public interface IState
{
    void Enter();

    void Exit();
}

public interface IUpdatableState : IState
{
    void Update();
}