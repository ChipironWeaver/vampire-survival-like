using System;
using TMPro;
using UnityEngine;

public class UITimeRender : MonoBehaviour
{
    [SerializeField] private Gradient _colorGradient;
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
        if (LevelController.GameIsRunning)
        {
            _timeText.color = _colorGradient.Evaluate(LevelController.CurrentGameTime / LevelController.MaxGameTime);
            _timeText.text = _beforeText + (int)LevelController.CurrentGameTime;
        }
    }
}
