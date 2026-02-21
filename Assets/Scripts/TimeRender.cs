using System;
using TMPro;
using UnityEngine;

public class TimeRender : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private string _beforeText;
    
    static TextMeshProUGUI _timeText;
    private void OnEnable()
    {
        _timeText = GetComponent<TextMeshProUGUI>();
        LevelController.onGameOver +=  HideText;
    }

    private void OnDisable()
    {
        LevelController.onGameOver -=  HideText;
    }
    
    private void HideText(bool _)
    {
        _timeText.text = "";
    }

    private void Update()
    {
        if (LevelController.gameIsRunning)
        {
            _timeText.color = Color.Lerp(_startColor, _endColor, LevelController.CurrentGameTime / LevelController.MaxGameTime);
            _timeText.text = _beforeText + (int)LevelController.CurrentGameTime;
        }
    }
}
