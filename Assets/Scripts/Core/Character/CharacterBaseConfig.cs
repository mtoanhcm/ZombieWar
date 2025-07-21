using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBaseConfig : ScriptableObject
    {
        [Header("Base attribute")]
        public CharacterID ID;
        public float MaxHealth;
        public float MovementSpeed;
        public float RotateSpeed;
    }
}
