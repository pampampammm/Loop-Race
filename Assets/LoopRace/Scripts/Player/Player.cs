using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private readonly LevelSwitcher _levelSwitcher;
    private readonly MainUIView _mainUIView;
    private readonly Camera _mainCamera;
    private readonly List<Garage> _garages;

    private bool _readyToFromDelay;
    private bool _garagesIsEmpty;

    public bool StopControl;

    public event Action EmptyGaragesLeft;
    
    

    public Player(LevelSwitcher levelSwitcher, MainUIView mainUIView, Camera mainCamera)
    {
        _levelSwitcher = levelSwitcher;
        _mainUIView = mainUIView;
        _mainCamera = mainCamera;
        _garages = new List<Garage>();

        StopControl = false;

        _readyToFromDelay = true;
    }

    public bool SwitchLevel(int index)
    {
        if (!_levelSwitcher.CanSwitch(index)) return false;

        var level = _levelSwitcher.Switch(index);

        if (level != null)
        {
            //level.Initialize(_mainCamera);
            CreateStack();

            foreach (var road in level.Roads)
            {
                road.MeshCreator.PathUpdate();
            }
        }

        return true;
    }

    public void TryReleaseCar(Garage garage)
    {
        if(StopControl) return;
        
        if (garage.State != GarageState.Empty)
        {
            garage.ReleaseCar();
            CalculateGarages();
        }
        
        if (_garagesIsEmpty) return;
        
        if (_readyToFromDelay == false)
            return;
        // if (_garagesStack.Count == 0)
        //     return;
        // if (!_garagesStack.Peek())
        //     return;
        //
        // var currentGarage = _garagesStack.Peek();
        //
        // if (currentGarage.State == GarageState.ReadyToRelease)
        // {
        //     currentGarage.ReleaseCar();
        //
        //     if (currentGarage.State == GarageState.Empty)
        //     {
        //         var pooped = _garagesStack.Pop();
        //         pooped.SetActiveColor(Color.red);
        //
        //         if (_garagesStack.Count == 0)
        //         {
        //             EmptyGaragesLeft?.Invoke();
        //             _garagesIsEmpty = true;
        //         }
        //         else
        //         {
        //             _garagesStack.Peek().SetActiveColor(Color.green);
        //         }
        //     }
        // }
        // else
        // {
        //     var pooped = _garagesStack.Pop();
        //     pooped.SetActiveColor(Color.red);
        //
        //     if (_garagesStack.Count == 0)
        //     {
        //         EmptyGaragesLeft?.Invoke();
        //         _garagesIsEmpty = true;
        //     }
        //     else
        //     {
        //         _garagesStack.Peek().SetActiveColor(Color.green);
        //     }
        // }
    }

    private void CalculateGarages()
    {
        var freeGarages = 0;
        
        foreach (var garage in _levelSwitcher.LevelCurrent.Garages)
        {
            if (garage.State == GarageState.ReadyToRelease)
            {
                freeGarages++;
            }
        }

        if (freeGarages == 0)
        {
            EmptyGaragesLeft?.Invoke();
        }
    }
    

    private void CreateStack()
    {
        _garages.Clear();
        
        foreach (var garage in _levelSwitcher.LevelCurrent.Garages)
        {
            if (garage.State == GarageState.ReadyToRelease)
            {
                _garages.Add(garage);
            }
        }
    }
}