using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    public class Health : MonoBehaviour
    {
        public float HealthValue
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;

                _healthBar?.UpdateHealth(_currentHealth, maxHealth);

                if (_currentHealth <= 0f)
                    Die();
            }
        }

        [Header("Settings")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HealthBar healthBarPrefab;
        [SerializeField] private Vector3 barOffset;

        private HealthBar _healthBar;
        private float _currentHealth;

        private void Awake() => _currentHealth = maxHealth;

        private void Start()
        {
            if (healthBarPrefab != null)
            {
                _healthBar = Instantiate(healthBarPrefab);
                _healthBar.Initialize(transform, barOffset);

                _healthBar.UpdateHealth(_currentHealth, maxHealth);
            }
        }

        private void Die()
        {
            _currentHealth = 0f;
            gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
        }
    }
}