using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelResourceDataLoaderImpl : IResourceDataService
{
    public Task GetList<T>(string path, out List<T> resource) where T : MonoBehaviour
    {
        var list = Resources.LoadAll<T>(path);

        resource = new List<T>(list);
        return Task.CompletedTask;
    }

}