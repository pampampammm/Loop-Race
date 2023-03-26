using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Garage : MonoBehaviour
{
    [SerializeField] private int _carCount = 1;
    [SerializeField] private Road road;
    [SerializeField] private Car _carPrefab;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private bool _createOnStart;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Color _enemyColor;

    private CarFabric _carFabric;
    private GarageState _garageState;
    private int _releasedCars;
    private List<Car> _cars;

    public GarageState State => _garageState;
    public int ReleasedCarsCount => _releasedCars;
    public int CarCount => _carCount;
    public event Action CarReleased;
    public bool CreateOnStart => _createOnStart;

    private void Awake()
    {
        _cars = new List<Car>();
        _releasedCars = 0;

        _garageState = _createOnStart ? GarageState.Empty : GarageState.ReadyToRelease;

        _carFabric = new CarFabric(road.Path);
    }

    private void Start()
    {
        CreateCarsOnStart();
    }

    public void ReleaseCar()
    {
        if (_garageState == GarageState.Empty)
            return;

        if (_releasedCars > 0)
            _cars[_releasedCars].gameObject.SetActive(true);

        _cars[_releasedCars].SetMoving(true);
        CarReleased?.Invoke();

        _releasedCars++;

        if (_releasedCars >= _carCount)
        {
            _garageState = GarageState.Empty;
        }
    }

    private void CreateCarsOnStart()
    {
        if (_createOnStart)
        {
            var newCar = BuildCar(_carPrefab, road.SpawnPoint, road.HasStopPoint);
            newCar.SetMoving(true);
            newCar.SetActiveColor(_enemyColor);
            return;
        }

        float spawnDelay = 0f;

        for (var index = 0; index < _carCount; index++)
        {
            var newCar = BuildCar(_carPrefab, road.SpawnPoint, road.HasStopPoint);
            
            _cars.Add(newCar);
            newCar.SetMoving(false);
            newCar.SetActiveColor(_colors[Random.Range(0, _colors.Count)]);

            if (index != 0)
                newCar.gameObject.SetActive(false);

            spawnDelay += 3;
        }
    }

    private Car BuildCar(Car car, float spawnPoint, bool hasStopPoint)
    {
        var rotation = Quaternion.identity;
        var newCar = _carFabric.CreateCarOnPath(car, spawnPoint, hasStopPoint, rotation);
        newCar.gameObject.SetActive(true);

        CarCrushObserver.Observe(newCar);

        return newCar;
    }
}