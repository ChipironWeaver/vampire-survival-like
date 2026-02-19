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
        
    
    private List<Image> _heartGroup = new List<Image>();
    private Transform _transform;
    private CharacterController _characterController;
    
    public delegate void OnPlayerDamage(int  health, int maxHealth);
    public static event OnPlayerDamage onPlayerDamage;
    
    public delegate void OnPlayerHeal(int  health, int maxHealth);
    public static event OnPlayerHeal onPlayerHeal;
    
    public delegate void OnDeath();
    public static event OnDeath onDeath;


    public void Start()
    {
        _transform = GetComponent<Transform>();
        onPlayerHeal?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!_invicible)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                onDeath?.Invoke();
            }
            else
            {
                _invicible = true;
                onPlayerDamage?.Invoke(_currentHealth, _maxHealth);
                Invoke(nameof(RemoveInvincibility), _invicibilityTime);
            }
            
        }
    }

    public void Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        onPlayerHeal?.Invoke(_currentHealth, _maxHealth);
    }

    private void RemoveInvincibility()
    {
        _invicible = false;
    }
}