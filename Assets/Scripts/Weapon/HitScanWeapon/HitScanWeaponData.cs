using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class HitScanWeaponData : WeaponBaseData
    {
        public ProjectileID BulletID { get; private set; }
        public int MaxMagazineAmmo { get; private set; }
        public int BulletPerShoot { get; private set; }
        public float ShootingRange { get; private set; }
        public float SpreadAngle { get; private set; }

        public Vector3 SnapHandPos { get; private set; }
        public Vector3 SnapHandRotEular { get; private set; }

        public HitScanWeaponData(HitScanWeaponConfig config) : base(config)
        {
            BulletID = config.BulletID;
            MaxMagazineAmmo = config.MaxMagazineAmmo;
            BulletPerShoot = config.BulletPerShoot;
            SnapHandPos = config.SnapPosition;
            SnapHandRotEular = config.SnapRotationEuler;
            ShootingRange = config.ShootingRange;
            SpreadAngle = config.SpreadAngle;
        }
    }
}
