using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Projectile;

namespace ZombieWar.Weapon
{
    public class HitScanWeapon : WeaponBase
    {
        [Header("Hitscan weapon attribute")]
        [SerializeField]
        private ProjectileSpawner firePoint;

        private MagazineAttachment magazine;
        private HitScanWeaponData hitScanWeaponData;
        private LayerMask targetLayerMask;
        private float tempShootDelayTime;

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

            if (tempShootDelayTime > Time.time) {
                return;
            }

            ShootBullet();

            tempShootDelayTime = Time.time + hitScanWeaponData.Cooldown;
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

        public override void SetOwner(ICombat owner)
        {
            this.owner = owner;
            targetLayerMask = ObjectLayer.TargetHitLayer(LayerMask.LayerToName(owner.Self.layer));
        }

        private void ShootBullet() {
            var shootDirection = ApplyRecoil(firePoint.transform.forward);
            var bulletMoveTime = 0.4f;

            var hitData = new HitData()
            {
                Damage = hitScanWeaponData.Damage,
                FirePos = firePoint.transform.position,
                TargetPos = firePoint.transform.position + shootDirection * hitScanWeaponData.ShootingRange,
                OnHitTarget = bullet => firePoint.ReturnBullet(bullet)
            };

            DebugCustom.DrawRay(firePoint.transform.position, shootDirection * hitScanWeaponData.ShootingRange, Color.red, 5f);

            if (Physics.Raycast(firePoint.transform.position, shootDirection, out var hit, hitScanWeaponData.ShootingRange, targetLayerMask))
            {
                bulletMoveTime = (hit.distance / (hitData.TargetPos - hitData.FirePos).magnitude) * bulletMoveTime;
                hitData.TargetPos = hit.point;

                if (hit.collider.TryGetComponent(out IHealth target))
                {
                    hitData.OnHitTarget = bullet =>
                    {
                        target.TakeDamage(hitData.Damage);
                        firePoint.ReturnBullet(bullet);
                    };
                }
            }

            var bullet = firePoint.SpawnBullet<StraightBulletProjectile>(false);
            bullet.Spawn(bulletMoveTime, hitData);
        }

        private Vector3 ApplyRecoil(Vector3 baseDirection) {
            float angle = Random.Range(-hitScanWeaponData.SpreadAngle, hitScanWeaponData.SpreadAngle);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return rotation * baseDirection;
        }
    }
}
