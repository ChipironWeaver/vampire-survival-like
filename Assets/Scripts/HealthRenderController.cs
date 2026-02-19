using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthRenderController : MonoBehaviour
{
    private List<Image> _heartGroup = new List<Image>();
    [Header("Render Settings")]
    [SerializeField] private Sprite _fullSprite;
    [SerializeField] private Sprite _emptySprite;

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
    
    private void RenderHealth(int currentHealth, int maxHealth)
    {
        while (_heartGroup.Count < maxHealth)
        {
            _heartGroup.Add(InstantiateHeart());
        }

        for (int i = 0; i < _heartGroup.Count; i++)
        {
            if (i > currentHealth - 1) _heartGroup[i].sprite =  _emptySprite;
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
