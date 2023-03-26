using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainUIView : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _gameplayButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private GameObject _tapScreen;
    [SerializeField] private GameObject _nextLevelScreen;
    [SerializeField] private GameObject _loseGameScreen;
    [SerializeField] private TMP_Text _carCount;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private SimpleTimer _timer;

    public Button ResetButton => _resetButton;
    public Button GameplayButton => _gameplayButton;
    public Button NextLevelButton => _nextLevelButton;
    public Button TryAgainButton => _tryAgainButton;
    public GameObject NextLevelScreen => _nextLevelScreen;
    public GameObject LoseGameScreen => _loseGameScreen;
    public GameObject TapScreen => _tapScreen;
    public SimpleTimer Timer => _timer;
    public TMP_Text CarCount => _carCount;

    public void UpdateCarCount(int count) => _carCount.text = count + "";

    public void DisableTapScreen() => _tapScreen.gameObject.SetActive(false);
    public void EnableTapScreen() => _tapScreen.gameObject.SetActive(true);

    public void SetLevel(int level) => _levelText.text = "Level " + level;

    public void StartTimer(float seconds)
    {
        StartCoroutine(_timer.StartTimer());
        _timer.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        _timer.Stop();
        _timer.gameObject.SetActive(false);
    }

    public void ShowCongradulationMenu()
    {
        _nextLevelScreen.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        _loseGameScreen.SetActive(true);
    }
}