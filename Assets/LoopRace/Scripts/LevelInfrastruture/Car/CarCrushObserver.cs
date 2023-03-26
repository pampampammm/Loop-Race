using System;
using System.Collections.Generic;
using CartoonFX;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public static class CarCrushObserver
{
    private static List<Car> _cars;
    private static CFXR_Effect _cfxrEffect;
    public static event Action CarSrushed;

    static CarCrushObserver()
    {
        _cars = new List<Car>();
    }

    public static void Observe(Car car)
    {
        if (_cars.Contains(car)) return;

        car.Crush += OnCarCrush;
    }

    private static void OnCarCrush(Car car)
    {
        CarSrushed?.Invoke();
        CreateBoomEffect(car);
    }

    private static void CreateBoomEffect(Car car)
    {
        var pos = car.transform.position;
        Object.Instantiate(_cfxrEffect, pos, quaternion.identity);
    }

    public static void SetEffect(CFXR_Effect cfxrEffectA)
    {
        _cfxrEffect = cfxrEffectA;
    }
}