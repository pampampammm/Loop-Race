
using CartoonFX;
using LoopRace.Scripts.Player;
using SupersonicWisdomSDK;
using UnityEngine;

public class GameBoostraper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private MainUIView _mainUIView;
    [SerializeField] private SaveData _data;
    
    
    [SerializeField] private Camera _camera;
    [SerializeField] private CFXR_Effect _cfxrEffect;
    [SerializeField] private PlayerInput _playerInput;
    private LoopRaceGame _game;
    
    void Awake()
    {
        SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
        SupersonicWisdom.Api.Initialize();
    }

    void OnSupersonicWisdomReady()
    {
        _game = new LoopRaceGame(this, _mainUIView, _playerInput,_cfxrEffect);
        _game.Start();

        DontDestroyOnLoad(this);
    }
    
    private void OnDisable()
    {
        _game.RemoveListeners();
    }
}