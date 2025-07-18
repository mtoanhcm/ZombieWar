using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBaseData
    {
        public WeaponID ID { get; private set; }
        public float Damage { get; private set; }
        public float Cooldown { get; private set; }

        public WeaponBaseData(WeaponBaseConfig config)
        {
            ID = config.ID;
            Damage = config.Damage;
            Cooldown = config.Cooldown;
        }
    }
}
