using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        public float CurrentHealth => currentHealth;

        public float MaxHealth => maxHealth;

        public bool IsAlive => currentHealth > 0;

        public Action OnDeath;

        private float currentHealth;
        private float maxHealth;

        public void Init(float maxHealth) { 
            currentHealth = maxHealth;
            this.maxHealth = maxHealth;
        }

        public void RestoreHealth(float amount)
        {
            currentHealth += Mathf.Abs(amount);
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= Mathf.Abs(damage);
            currentHealth = Mathf.Max(currentHealth, 0);

            if (currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
