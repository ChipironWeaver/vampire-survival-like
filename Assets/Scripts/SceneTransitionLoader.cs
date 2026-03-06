using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private bool _autoFadeIn;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private Color _fadeColor =  Color.white;

    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        if  (_autoFadeIn) FadeIn();
    }

    public void FadeOut(string sceneName)
    {
        StartCoroutine(FadeCoroutine(Color.clear, _fadeColor, sceneName));
    }

    private void FadeIn()
    {
        StartCoroutine(FadeCoroutine(_fadeColor, Color.clear));
    }

    private IEnumerator FadeCoroutine(Color firstColor, Color secondColor, string SceneName = null)
    {
        float time = 0;
        while (time < _fadeDuration)
        {
            _image.color = Color.Lerp(firstColor, secondColor, time / _fadeDuration);
            time += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
        if (SceneName != null) 
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
