using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIEnterExitAnimation : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;
    [Header("Visual")]
    [SerializeField] private Sprite _spriteWin;
    [SerializeField] private Sprite _spriteLose;
    [SerializeField] private Color _textWinColor;
    [SerializeField] private Color _textLoseColor;
    [SerializeField] private string _loseText;
    [SerializeField] private string _winText;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _winLoseText;
    [SerializeField] private TextMeshProUGUI _replayText;
    [SerializeField] private Image _replayButton;
    
    
    private Vector2 _target;
    private Transform _transform;
    private Image _image;
    private void OnEnable()
    {
        _transform = GetComponent<Transform>();
        _image = GetComponent<Image>();
        LevelController.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        LevelController.onGameOver -= GameOver;
    }
    
    public void GameOver(bool win)
    {
        Sprite buttonSprite = win ? _spriteWin : _spriteLose;
        _image.sprite = buttonSprite;
        _replayButton.sprite = buttonSprite;
        
        Color textColor = win ? _textWinColor : _textLoseColor;
        _scoreText.color = textColor;
        _replayText.color = textColor;
        _winLoseText.color = textColor;
        
        int _score = (int)(Score.score * Score.scoreMultiplier);
        _scoreText.SetText(_score.ToString());
            
        _winLoseText.SetText(win ? _winText : _loseText);
        StartCoroutine(SmoothMove(Vector2.zero, new Vector2(Screen.height * _direction.x * 2, Screen.width * _direction.y * 2), _speed));
    }
    public void FadeOut()
    {
        StartCoroutine(SmoothMove(new Vector2(Screen.height * _direction.x * 2, Screen.width * _direction.y * 2),Vector2.zero, _speed, true));
    }
    IEnumerator SmoothMove(Vector2 target , Vector2 startingPosition, float duration, bool disable = false)
    {
        float time = 0;
        if (disable)
        {
            while (time < duration)
            {
                _transform.localPosition = Vector2.Lerp(startingPosition, target, time / duration);
                time += Time.deltaTime;
                yield return new WaitForNextFrameUnit();
            }
        }
        else
        {
            while (time < duration)
            {
                _transform.localPosition = Vector2.Lerp(startingPosition, target, 1 - (Mathf.Pow(time / duration -1,5) *-1)  ); 
                time += Time.deltaTime;
                yield return new WaitForNextFrameUnit();
            }
        }
    }
}
