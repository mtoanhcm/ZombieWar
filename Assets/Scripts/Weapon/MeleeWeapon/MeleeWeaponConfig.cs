using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    [CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Config/Weapon/MeleeWeaponConfig")]
    public class MeleeWeaponConfig : WeaponBaseConfig
    {
        [Header("Melee weapon attribute")]
        public float AttackRange;
        public float AttackAngle;
    }
}
