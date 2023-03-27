using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class Road : MonoBehaviour
{
    [Header("Настрйоки")]

    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private RoadMeshCreator _roadMeshCreator;
    [SerializeField] private float _spawnPoint;
    
    [Header("Светофор")]
    
    [SerializeField] private bool _hasStopPoint;

    public float SpawnPoint => _spawnPoint;
    public bool HasStopPoint => _hasStopPoint;
    public PathCreator Path => _pathCreator;
    public RoadMeshCreator MeshCreator => _roadMeshCreator;

    private void OnDrawGizmos()
    {
        if (_pathCreator == null) return;

        var newPoint = _pathCreator.path.GetPointAtDistance(_spawnPoint);
        var nextPoint = _pathCreator.path.GetPointAtDistance(_spawnPoint + 1);
        
        Gizmos.DrawSphere(newPoint, 0.40f);
        Gizmos.DrawSphere(nextPoint, 0.25f);
    }
}