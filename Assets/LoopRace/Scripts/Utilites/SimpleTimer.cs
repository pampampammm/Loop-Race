using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTimer : MonoBehaviour
{
    //[SerializeField] private TMP_Text timerText;
    [SerializeField] private Slider _timerSlider;
    
    [SerializeField] private float _time = 5f;
    private float _timeLeft = 0f;
    private bool _timerIsEnd;

    public bool TimerEnd => _timerIsEnd;

    public IEnumerator StartTimer()
    {
        _timeLeft = _time;

        while (_timeLeft > 0)
        {
            UpdateSlider();
            yield return null;
        }
    }

    private void Update()
    {
        if(_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            _timerIsEnd = true;
        }
    }

    public void Stop()
    {
        _timeLeft = _time;
    }

    private void UpdateSlider()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        var time = _time / 100;
        var newTime = _timeLeft / time; 
        
        _timerSlider.value = newTime / 100;
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        //timerText.text = string.Format("{1:00}", minutes, seconds);
    }
}