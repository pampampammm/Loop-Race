using UnityEngine;

public class GarageEmptyState : IState
{
    private readonly Garage _garage;
    private GarageState _garageState;

    public GarageEmptyState(Garage garage, GarageState garageState)
    {
        _garage = garage;
        _garageState = garageState;
    }

    public void Enter()
    {
        Recolor(Color.cyan);

        _garageState = GarageState.Empty;
        Debug.Log("Ready empty " + _garage.gameObject.name);
    }

    public void Exit()
    {
        Recolor(Color.blue);
    }

    private void Recolor(Color color) => 
        _garage.gameObject.GetComponent<MeshRenderer>().material.color = color;
}