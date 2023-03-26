using System;
using UnityEngine;

public class GarageReadyToReleaseState : IState
{
    private readonly Garage _garage;
    private GarageState _garageState;

    public GarageReadyToReleaseState(Garage garage, GarageState garageState)
    {
        _garage = garage;
        _garageState = garageState;
    }

    public void Enter()
    {
        Recolor(Color.cyan);
        _garageState = GarageState.ReadyToRelease;
    }

    public void Exit()
    {
        Recolor(Color.blue);
    }

    private void Recolor(Color color) => 
        _garage.gameObject.GetComponent<MeshRenderer>().material.color = color;
}