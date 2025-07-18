using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBaseData
    {
        public CharacterID ID { get; private set; }
        public int Level { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float MovementSpeed { get; private set; }

        public CharacterBaseData(CharacterBaseConfig config)
        {
            ID = config.ID;
            Level = 1;
            MaxHealth = config.MaxHealth;
            CurrentHealth = config.MaxHealth; // Initialize current health to max health
            MovementSpeed = config.MovementSpeed;
        }

        public void LevelUp()
        {
            Level++;

            //TODO: Upgrade stats here!!
        }

        public void UpdateHealth(float amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        }
    }
}
