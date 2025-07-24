using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    [CreateAssetMenu(fileName = "HitScanWeaponConfig", menuName = "Config/Weapon/HitScanWeaponConfig")]
    public class HitScanWeaponConfig : WeaponBaseConfig
    {
        [Header("Hit scan weapon attribute")]
        public ProjectileID BulletID;
        public int MaxMagazineAmmo;
        public int BulletPerShoot;
        public float ShootingRange;
        public float SpreadAngle;

        [Header("Hardcode snap position to main character hand")]
        public Vector3 SnapPosition;
        public Vector3 SnapRotationEuler;
    }
}
