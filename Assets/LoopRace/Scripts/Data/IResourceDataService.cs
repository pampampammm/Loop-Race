using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IResourceDataService
{
    Task GetList<T>(string path, out List<T> resource) where T : MonoBehaviour;
}