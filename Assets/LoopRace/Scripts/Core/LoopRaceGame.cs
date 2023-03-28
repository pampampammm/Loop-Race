using System;
using System.Collections;
using System.Threading.Tasks;
using CartoonFX;
using LoopRace.Scripts.Player;
using SupersonicWisdomSDK;
using UnityEngine;
using UnityEngine.UI;

public class LoopRaceGame : Game
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly MainUIView _mainUIView;
    private readonly PlayerInput _playerInput;
    private readonly CFXR_Effect _cfxrEffectA;
    private readonly Camera _mainCamera;
    private readonly IResourceDataService _resourceDataService;

    private int _currentLevelIndex = 0;
    private Player _player;
    private LevelSwitcher _levelSwitcher;
    private Coroutine _coroutine;
    private bool _gameIsEnd;
    private int _pastLevelIndex;

    private const string LEVELTRANSFORM = "LevelTransform";

    public LoopRaceGame(ICoroutineRunner coroutineRunner, MainUIView mainUIView, PlayerInput playerInput,
        CFXR_Effect _cfxrEffectA)
    {
        _coroutineRunner = coroutineRunner;
        _mainUIView = mainUIView;
        _playerInput = playerInput;
        this._cfxrEffectA = _cfxrEffectA;
        _resourceDataService = new LevelResourceDataLoaderImpl();
    }

    public override async Task Start()
    {
        await ExecuteDataShit();
        InitializeGameplayFeatures();
    }

    public override void Update()
    {
    }

    private async Task ExecuteDataShit()
    {
        await _resourceDataService.GetList<Level>(path: "Levels", out var levelDataList);

        foreach (var level in levelDataList)
        {
            if (level.GetComponent<Level>().Garages.Count == 0)
            {
                throw new Exception("Level " + level.name + " not contains garages");
            }
        }

        _currentLevelIndex = PlayerPrefs.GetInt("Level");

        var levelTransform = GameObject.FindGameObjectWithTag(LEVELTRANSFORM).transform;
        _levelSwitcher = new LevelSwitcher(levelDataList, levelTransform, _coroutineRunner, 1f);
    }

    private void InitializeGameplayFeatures()
    {
        _player = new Player(_levelSwitcher, _mainUIView, _mainCamera);
        _player.SwitchLevel(_currentLevelIndex);
        _pastLevelIndex = _currentLevelIndex;
        _mainUIView.SetLevel(_currentLevelIndex + 1);

        CarCrushObserver.SetEffect(_cfxrEffectA);

        RegisterListeners();
    }

    private void RegisterListeners()
    {
        _mainUIView.ResetButton.onClick.AddListener(Reset);
        _mainUIView.NextLevelButton.onClick.AddListener(TrySwitchLevelByPlayer);
        _mainUIView.TryAgainButton.onClick.AddListener(Reset);

        _playerInput.GarageClicked += _player.TryReleaseCar;
        CarCrushObserver.CarSrushed += Lose;

        _player.EmptyGaragesLeft += StartTimer;
    }

    public void RemoveListeners()
    {
        _mainUIView.ResetButton.onClick.RemoveListener(Reset);
        _mainUIView.NextLevelButton.onClick.RemoveListener(TrySwitchLevelByPlayer);
        _mainUIView.TryAgainButton.onClick.RemoveListener(TrySwitchLevelByPlayer);

        _playerInput.GarageClicked -= _player.TryReleaseCar;
        CarCrushObserver.CarSrushed -= Lose;

        _player.EmptyGaragesLeft -= StartTimer;
    }

    private void TrySwitchLevelByPlayer()
    {
        _pastLevelIndex = _currentLevelIndex;
        _currentLevelIndex++;

        if (_currentLevelIndex >= _levelSwitcher.Levels.Count)
        {
            _player.SwitchLevel(0);
            _currentLevelIndex = 0;
            PlayerPrefs.DeleteAll();
        }
        else
        {
            _player.SwitchLevel(_currentLevelIndex);


            ExecuteLevelStartIvent();
            PlayerPrefs.SetInt("Level", _currentLevelIndex);
            PlayerPrefs.Save();
        }

        _mainUIView.NextLevelScreen.SetActive(false);

        _mainUIView.SetLevel(_currentLevelIndex + 1);
    }

    private void StartTimer()
    {
        if (_coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);

        _coroutine = _coroutineRunner.StartCoroutine(StartTimerToWin(12));
    }

    private IEnumerator StartTimerToWin(float seconds)
    {
        _gameIsEnd = false;

        _mainUIView.StartTimer(seconds);

        yield return _mainUIView.Timer.StartTimer();

        if (_gameIsEnd == false)
        {
            Win();
        }
    }

    private void Win()
    {
        SupersonicWisdom.Api.NotifyLevelCompleted(_currentLevelIndex, null);

        _mainUIView.ShowCongradulationMenu();
        _mainUIView.StopTimer();
        if (_coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);

        _gameIsEnd = true;
        StopGame();
    }

    private void Lose()
    {
        SupersonicWisdom.Api.NotifyLevelFailed(_currentLevelIndex, null);

        _gameIsEnd = false;
        _mainUIView.StopTimer();
        if (_coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);

        _mainUIView.LoseGameScreen.SetActive(false);
        _mainUIView.NextLevelScreen.SetActive(false);

        _mainUIView.ShowLoseMenu();
    }

    private void StopGame()
    {
        var cars = GameObject.FindGameObjectsWithTag("Car");

        _mainUIView.StopTimer();

        if (_coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);

        foreach (var car in cars)
        {
            car.GetComponent<Car>().SetMoving(false);
        }
    }

    private void Reset()
    {
        _gameIsEnd = true;
        _mainUIView.LoseGameScreen.SetActive(false);
        _mainUIView.NextLevelScreen.SetActive(false);
        
        _mainUIView.Timer.Stop();
        
        _player.SwitchLevel(_currentLevelIndex);
    }

    private void ExecuteLevelStartIvent()
    {
        SupersonicWisdom.Api.NotifyLevelStarted(_currentLevelIndex, null);
    }
}