using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [Header("Invincibility Frame Settings")]
    [SerializeField] private float _invicibilityTime;
    [SerializeField] private bool  _invicible;
    [Header("Render Settings")]
    [SerializeField] private Sprite _fullSprite;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private GameObject _group;
        
    
    private List<Image> _heartGroup = new List<Image>();
    private Transform _transform;
    private CharacterController _characterController;


    public void Start()
    {
        _currentHealth = _maxHealth;
        RenderHealth(); 
        _transform = GetComponent<Transform>();
    }

    public void TakeDamage(int damage)
    {
        if (!_invicible)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Death();
            }
            _invicible = true;
            Invoke(nameof(RemoveInvincibility), _invicibilityTime);
        }
        RenderHealth();
    }

    public void Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        RenderHealth();
    }

    public void Death()
    {
        print("death");
        
    }

    private void RemoveInvincibility()
    {
        _invicible = false;
    }

    private void RenderHealth()
    {
        while (_heartGroup.Count < _maxHealth)
        {
            _heartGroup.Add(InstantiateHeart());
        }

        for (int i = 0; i < _heartGroup.Count; i++)
        {
            if (i > _currentHealth - 1) _heartGroup[i].sprite =  _emptySprite;
            else _heartGroup[i].sprite = _fullSprite;;
        }
    }

    private Image InstantiateHeart()
    {
        GameObject Heart = new GameObject();
        Heart.transform.SetParent(_group.transform);
        Heart.name = "Heart" + (_heartGroup.Count + 1) ;
        Heart.AddComponent<RectTransform>().localScale = Vector3.one;
        return Heart.AddComponent<Image>();
    }
}