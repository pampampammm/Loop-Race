using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera;
    
    [SerializeField] 
    private Transform _marker;

    [SerializeField] 
    private Transform _target;

    [Space, SerializeField] 
    private float _offset = 100;
    
    private Vector3 _screenCenter;
    private Vector3 _screenBorder;


    private void Update()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(_target.position);
        Debug.Log(screenPoint);

    }
    
}

public static class CameraExtension
{
    public static bool IsVisiblePoint(this Camera camera, Vector3 screenPoint, float offset) =>
        screenPoint.z > 0 && screenPoint.x - offset > 0 && screenPoint.x + offset < camera.pixelWidth && screenPoint.y - offset > 0 && screenPoint.y + offset < camera.pixelHeight;
}