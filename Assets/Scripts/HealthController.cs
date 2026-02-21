using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;
    [Header("Invincibility Frame Settings")]
    [SerializeField] private float _invicibilityTime;
    [SerializeField] private bool  _invicible;
        
    
    private List<Image> _heartGroup = new List<Image>();
    private Transform _transform;
    private CharacterController _characterController;
    
    public delegate void OnPlayerDamage();
    public static event OnPlayerDamage onPlayerDamage;
    
    public delegate void OnPlayerHeal();
    public static event OnPlayerHeal onPlayerHeal;


    public void OnEnable()
    {
        _transform = GetComponent<Transform>();
        LevelController.onStartGame += GameStart;

    }

    public void OnDisable()
    {
        LevelController.onStartGame -= GameStart;
    }

    public void GameStart()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!_invicible)
        {
            currentHealth -= damage;
            onPlayerDamage?.Invoke();
            if (currentHealth <= 0)
            {
                LevelController.GameOver(false);
            }
            else
            {
                _invicible = true;
                Invoke(nameof(RemoveInvincibility), _invicibilityTime);
            }
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        onPlayerHeal?.Invoke();
    }

    private void RemoveInvincibility()
    {
        _invicible = false;
    }
}