using UnityEngine;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
public class UIAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float transitionTime;
    [SerializeField] private GameObject _target;
    private float currentTime;
    private int  currentFrame;
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
            _target.GetComponent<Image>().sprite = frames[currentFrame];
        }
    }
}
