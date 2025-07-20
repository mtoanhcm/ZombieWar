using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class HitScanWeaponData : WeaponBaseData
    {
        public ProjectileID BulletID { get; private set; }
        public int MaxMagazineAmmo { get; private set; }
        public int AmmoPerShoot { get; private set; }

        public Vector3 SnapHandPos { get; private set; }
        public Vector3 SnapHandRotEular { get; private set; }

        public HitScanWeaponData(HitScanWeaponConfig config) : base(config)
        {
            BulletID = config.BulletID;
            MaxMagazineAmmo = config.MaxMagazineAmmo;
            AmmoPerShoot = config.AmmoPerShoot;
            SnapHandPos = config.SnapPosition;
            SnapHandRotEular = config.SnapRotationEuler;
        }
    }
}
