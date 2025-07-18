using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBaseConfig : ScriptableObject
    {
        public WeaponID ID;
        public float Damage;
        public float Cooldown;
    }
}
