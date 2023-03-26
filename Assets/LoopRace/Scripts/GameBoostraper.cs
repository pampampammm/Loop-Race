
using CartoonFX;
using LoopRace.Scripts.Player;
using UnityEngine;

public class GameBoostraper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private MainUIView _mainUIView;
    [SerializeField] private SaveData _data;
    [SerializeField] private Camera _camera;
    [SerializeField] private CFXR_Effect _cfxrEffect;
    [SerializeField] private PlayerInput _playerInput;
    private LoopRaceGame _game;

    private void Awake()
    {
        _game = new LoopRaceGame(_data, this, _mainUIView, _playerInput,_cfxrEffect);
        _game.Start();

        DontDestroyOnLoad(this);
    }

    private void ClearTrash()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("RaodMesh"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        _game.RemoveListeners();
    }
}