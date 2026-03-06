using UnityEngine;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
public class UIAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float transitionTime = 0.5f;
    [SerializeField] private Image _target;
    private float currentTime;
    private int  currentFrame;

    void Start()
    {
        if (_target == null) 
        {
            _target = GetComponent<Image>();
        }
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= transitionTime)
        {
            currentTime -= transitionTime;
            currentFrame++;
            if (currentFrame == frames.Count)
            {
                currentFrame = 0;
            }
            _target.sprite = frames[currentFrame];
        }
    }
}
