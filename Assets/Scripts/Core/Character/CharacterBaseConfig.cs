using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBaseConfig : ScriptableObject
    {
        public CharacterID ID;
        public float MaxHealth;
        public float MovementSpeed;
    }
}
