using PathCreation;
using UnityEngine;

public class CarFabric
{
    private readonly PathCreator _carPath;
    private readonly Transform _transform;

    public CarFabric(PathCreator carPath, Transform transform)
    {
        _carPath = carPath;
        _transform = transform;
    }

    public Car CreateCarOnPath(Car car, float distanceSpawnPoint, bool hasStopPoint, Quaternion rotation)
    {
        var pos = _carPath.path.GetPointAtDistance(distanceSpawnPoint);
        var instantiate = Object.Instantiate(car, pos, rotation);
        
        instantiate.gameObject.SetActive(false);
        instantiate.SetPath(_carPath, distanceSpawnPoint, hasStopPoint);
        instantiate.gameObject.SetActive(true);
        
        instantiate.transform.SetParent(_transform);

        return instantiate;
    }
}