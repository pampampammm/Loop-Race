using System.Threading.Tasks;
using UnityEngine;

public abstract class Game 
{
    protected Game()
    {
    } 

    public abstract Task Start();

    public abstract void Update();
}