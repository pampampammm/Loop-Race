using UnityEngine;

public class LoopGameSettings : ScriptableObject
{
    [SerializeField] private float _switchDelay;
    [SerializeField] private float _carsSpeed;


    public float CarsSpeed => _carsSpeed; 
    public float SwitchDelay => _switchDelay;
}