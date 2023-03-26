using System.Collections.Generic;

public class LevelState
{
    private readonly List<Garage> _garages;
    public IReadOnlyList<Garage> Garages => _garages;

    public LevelState(List<Garage> garages)
    {
        _garages = garages;
    }
}