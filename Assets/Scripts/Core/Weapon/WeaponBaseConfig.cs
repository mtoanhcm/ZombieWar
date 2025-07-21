using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBaseConfig : ScriptableObject
    {
        [Header("Base attribute")]
        public WeaponID ID;
        public WeaponType Type;
        public float Damage;
        public float Cooldown;
    }
}
