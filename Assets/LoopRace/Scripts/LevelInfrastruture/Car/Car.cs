using System;
using System.Collections;
using PathCreation;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _colliderSize;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private MeshRenderer _meshRenderer;

    private PathCreator _carPath;
    private bool _carCrushed;
    private float _arrivedDistance;
    private bool _needToMove = true;
    private bool _lightIn;

    private bool _hasStopPoint;
    private bool _getReadyToFromOfPoint;

    private float _pointToStop;
    private float _newPointToStop;
    private float _roadLenght;

    public event Action<Car> Crush;

    public void Update()
    {
        if(_hasStopPoint == false) return;
        
        if (Math.Abs(_arrivedDistance - _newPointToStop) < 0.1f)
        {
            Debug.Log("я вашла бля");
            StartCoroutine(StartTimeCoroutine(2f));
            _getReadyToFromOfPoint = false;
            _newPointToStop += _roadLenght + _pointToStop;
        }
    }

    private void FixedUpdate()
    {
        //if (_getReadyToFromOfPoint) return;
        if (_needToMove == false) return;
        if (_carCrushed) return;

        CheckCollisions();
        Move();
    }

    private void Start()
    {
        var newRot = _carPath.path.GetRotationAtDistance(_arrivedDistance);
        var asf = newRot.eulerAngles;
        asf.z = 0f;

        transform.rotation = Quaternion.Euler(asf);
    }

    public void SetMoving(bool flag)
    {
        _needToMove = flag;
    }

    public void SetActiveColor(Color color)
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = color;
    }

    private void Move()
    {
        if (_carPath == null)
            return;

        _arrivedDistance += (_speed + Time.deltaTime) * Time.deltaTime;
        var newPos = transform.position = _carPath.path.GetPointAtDistance(_arrivedDistance);
        var newRot = _carPath.path.GetRotationAtDistance(_arrivedDistance);

        var asf = newRot.eulerAngles;
        asf.z = 0f;

        transform.position = newPos;
        transform.rotation = Quaternion.Euler(asf);
    }

    private IEnumerator StartTimeCoroutine(float timeValue)
    {
        SetMoving(false);
        yield return new WaitForSeconds(timeValue);
        SetMoving(true);
    }

    public void SetPath(PathCreator path, float pos, bool hasStopPoint)
    {
        _hasStopPoint = hasStopPoint;

        _carPath = path;
        transform.position = _carPath.path.GetPointAtDistance(pos);
        _arrivedDistance = pos;
    }

    private void CheckCollisions()
    {
        var results = Physics.OverlapSphere(transform.position, _colliderSize.x);

        foreach (var collider in results)
        {
            if (collider == GetComponent<Collider>()) continue;

            if (collider.TryGetComponent<Car>(out var component))
            {
                component.Crush?.Invoke(this);
                _carCrushed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasStopPoint == false) return;
        

        if (other.CompareTag("stop"))
        {
            StartCoroutine(StartTimeCoroutine(2f));
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position, _colliderSize);
        Gizmos.DrawWireSphere(transform.position, _colliderSize.x);
    }
}