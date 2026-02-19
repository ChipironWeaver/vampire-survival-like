using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthRenderController : MonoBehaviour
{
    private List<Image> _heartGroup = new List<Image>();
    [Header("Render Settings")]
    [SerializeField] private Sprite _fullSprite;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private HealthController _healthController;

    private void OnEnable()
    {
        HealthController.onPlayerDamage += RenderHealth;
        HealthController.onPlayerHeal += RenderHealth;
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= RenderHealth;
        HealthController.onPlayerHeal -= RenderHealth;
    }

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        Invoke(nameof(RenderHealth), 0.1f);
    }
    
    private void RenderHealth()
    {
        while (_heartGroup.Count < _healthController.maxHealth)
        {
            _heartGroup.Add(InstantiateHeart());
        }

        for (int i = 0; i < _heartGroup.Count; i++)
        {
            if (i > _healthController.currentHealth - 1) _heartGroup[i].sprite =  _emptySprite;
            else _heartGroup[i].sprite = _fullSprite;;
        }
    }

    private Image InstantiateHeart()
    {
        GameObject Heart = new GameObject();
        Heart.transform.SetParent(transform);
        Heart.name = "Heart" + (_heartGroup.Count + 1) ;
        Heart.AddComponent<RectTransform>().localScale = Vector3.one;
        return Heart.AddComponent<Image>();
    }
}
