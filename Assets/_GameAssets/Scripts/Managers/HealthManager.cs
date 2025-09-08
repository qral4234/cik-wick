using UnityEngine;
using System;


public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    private void Awake()
    {
        // Singleton ataması
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _currentHealth = _maxHealth;
    }
    
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int amount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= amount;
            _playerHealthUI.AnimateDamage();
        }

        if (_currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }
    public int GetCurrentHealth() => _currentHealth;
}