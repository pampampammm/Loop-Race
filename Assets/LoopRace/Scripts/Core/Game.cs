using System.Threading.Tasks;
using UnityEngine;

public abstract class Game 
{
    private readonly SaveData _saveData;

    protected Game(SaveData saveData)
    {
        _saveData = saveData;
    }

    public abstract Task Start();

    public abstract void Update();
}