using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class HitScanWeapon : WeaponBase<HitScanWeaponData>
    {
        private MagazineAttachment magazine;

        public override void Spawn(HitScanWeaponData weaponData)
        {
            base.Spawn(weaponData);

            AddAttachment();
        }

        public override void AddAttachment()
        {
            magazine = new MagazineAttachment(weaponData.MaxMagazineAmmo);
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
            magazine.AddAmmo(weaponData.MaxMagazineAmmo);
        }

        public void SnapToHandGrabPoint(Transform grabPoint)
        {
            transform.SetParent(grabPoint);

            transform.SetLocalPositionAndRotation(
                weaponData.SnapHandPos, 
                Quaternion.Euler(weaponData.SnapHandRotEular));
        }
    }
}
