using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class MeleeWeaponData : WeaponBaseData
    {
        public float AttackRange { get; private set; }
        public float AttackAngle { get; private set; }

        public MeleeWeaponData(MeleeWeaponConfig config) : base(config)
        {
            AttackRange = config.AttackRange;
            AttackAngle = config.AttackAngle;
        }
    }
}
