using PathCreation;
using UnityEngine;

public class CarFabric
{
    private readonly PathCreator _carPath;

    public CarFabric(PathCreator carPath)
    {
        _carPath = carPath;
    }

    public Car CreateCarOnPath(Car car, float distanceSpawnPoint, bool hasStopPoint, Quaternion rotation)
    {
        var pos = _carPath.path.GetPointAtDistance(distanceSpawnPoint);
        var instantiate = Object.Instantiate(car, pos, rotation);
        
        instantiate.gameObject.SetActive(false);
        instantiate.SetPath(_carPath, distanceSpawnPoint, hasStopPoint);
        instantiate.gameObject.SetActive(true);

        return instantiate;
    }
}