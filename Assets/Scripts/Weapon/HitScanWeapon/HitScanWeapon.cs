using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class HitScanWeapon : WeaponBase
    {
        private MagazineAttachment magazine;
        private HitScanWeaponData hitScanWeaponData;

        public override void Spawn<T>(T weaponData)
        {
            base.Spawn(weaponData);

            hitScanWeaponData = weaponData as HitScanWeaponData;
            magazine = new MagazineAttachment(hitScanWeaponData.MaxMagazineAmmo);
        }

        public override bool AddAttachment()
        {
            return true;
        }

        public override void Attack()
        {
            if (!magazine.HasAmmo) {
                Reload();
                return;
            }


        }

        public void Reload()
        {
            //Infinite ammo
            magazine.AddAmmo(hitScanWeaponData.MaxMagazineAmmo);
        }

        public void SnapToHandGrabPoint(Transform grabPoint)
        {
            transform.SetParent(grabPoint);

            transform.SetLocalPositionAndRotation(
                hitScanWeaponData.SnapHandPos, 
                Quaternion.Euler(hitScanWeaponData.SnapHandRotEular));
        }
    }
}
