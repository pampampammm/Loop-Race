using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelSwitcher
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly Transform _transform;
    private readonly List<Level> _levels;
    private readonly float _switchDelay;
    private Level _currentPreFab;
    private const string ROADMESH = "RoadMesh";
    private const string CAR = "Car";


    public IReadOnlyList<Level> Levels => _levels;
    public LevelState LevelCurrent { get; private set; }

    public LevelSwitcher(List<Level> levels, Transform transform, ICoroutineRunner coroutineRunner, float switchDelay)
    {
        _levels = levels;
        _transform = transform;
        _coroutineRunner = coroutineRunner;
        _switchDelay = switchDelay;
    }

    public bool CanSwitch(int index)
    {
        return index < _levels.Count;
    }

    public Level Switch(int levelIndex)
    {
        var level = BuildLevel(levelIndex, _transform);

        DisappearCurrentLevel();
        _currentPreFab = level;
        LevelCurrent = level.Get();
        
        return level;
    }

    private void DisappearCurrentLevel()
    {
        if (_currentPreFab != null || LevelCurrent != null)
        {
            Object.Destroy(_currentPreFab.gameObject);

            var meshes = GameObject.FindGameObjectsWithTag(ROADMESH);
            var cars = GameObject.FindGameObjectsWithTag(CAR);
            foreach (var mesh in meshes)
            {
                Object.Destroy(mesh);
            }

            foreach (var car in cars)
            {
                Object.Destroy(car);
            }
        }
    }

    private Level BuildLevel(int levelIndex, Transform transform)
    {
        var levelGameObject = Object.Instantiate(_levels[levelIndex], transform);
        transform.SetParent(levelGameObject.transform);

        return levelGameObject.GetComponent<Level>();
    }
}