using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Garage> _garage;
    [SerializeField] private Road[] roads;
    
    [CanBeNull] public Road[] Roads => roads;
    public List<Garage> Garages => _garage;

    public LevelState Get() =>
        _garage.Count == 0 ? null : new LevelState(_garage);
    
}
