using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using ZombieWar.Core;
using ZombieWar.Projectile;

namespace ZombieWar.Weapon
{
    public class HitScanWeapon : WeaponBase
    {
        [Header("Hitscan weapon attribute")]
        [SerializeField]
        private ProjectileSpawner firePoint;
        [SerializeField]
        private GameObject muzzleVFX;
        [SerializeField]
        private AudioSource fireSource;

        private MagazineAttachment magazine;
        private HitScanWeaponData hitScanWeaponData;
        private LayerMask targetLayerMask;
        private float tempShootDelayTime;

        public override void Spawn<T>(T weaponData)
        {
            muzzleVFX.SetActive(false);

            base.Spawn(weaponData);

            hitScanWeaponData = weaponData as HitScanWeaponData;
            magazine = new MagazineAttachment(hitScanWeaponData.MaxMagazineAmmo);
        }

        public override bool AddAttachment()
        {
            return true;
        }

        public override void Attack(bool isAttack)
        {
            if (!magazine.HasAmmo) {
                muzzleVFX.SetActive(false);
                Reload();
                return;
            }

            if (tempShootDelayTime > Time.time) {
                return;
            }

            muzzleVFX.SetActive(false);
            muzzleVFX.SetActive(true);

            fireSource.Play();
            if (hitScanWeaponData.BulletPerShoot <= 1)
            {
                ShootSingleBullet();
            } else
            {
                ShootMultiBullet(hitScanWeaponData.BulletPerShoot);
            }

            tempShootDelayTime = Time.time + hitScanWeaponData.Cooldown;
        }

        public void Reload()
        {
            //Infinite ammo
            magazine.AddAmmo(hitScanWeaponData.MaxMagazineAmmo);
        }

        public override void SnapToHandGrabPoint(Transform grabPoint)
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

        private void ShootMultiBullet(int bullets)
        {
            List<Vector3> directions = new List<Vector3>();
            float startAngle;
            float step;

            if (bullets % 2 == 1)
            {
                int half = bullets / 2;
                step = (hitScanWeaponData.SpreadAngle * 2f) / (bullets - 1);
                startAngle = -hitScanWeaponData.SpreadAngle;

                for (int i = 0; i < bullets; i++)
                {
                    float currentAngle = startAngle + step * i;
                    Vector3 dir = Quaternion.AngleAxis(currentAngle, Vector3.up) * firePoint.transform.forward;
                    directions.Add(dir);
                }
            }
            else
            {
                step = (hitScanWeaponData.SpreadAngle * 2f) / bullets;
                startAngle = -hitScanWeaponData.SpreadAngle + step / 2f;

                for (int i = 0; i < bullets; i++)
                {
                    float currentAngle = startAngle + step * i;
                    Vector3 dir = Quaternion.AngleAxis(currentAngle, Vector3.up) * firePoint.transform.forward;
                    directions.Add(dir);
                }
            }

            foreach(var direct in directions)
            {
                var hitData = new HitData()
                {
                    Damage = hitScanWeaponData.Damage,
                    FirePos = firePoint.transform.position,
                    TargetPos = firePoint.transform.position + direct * hitScanWeaponData.ShootingRange,
                    OnHitTarget = bullet => firePoint.ReturnBullet(bullet)
                };

                ShootBullet(hitData, 0.4f, direct);
            }
        }

        private void ShootSingleBullet() {
            var shootDirection = ApplyRecoil(firePoint.transform.forward);
            var bulletMoveTime = 0.4f;

            var hitData = new HitData()
            {
                Damage = hitScanWeaponData.Damage,
                FirePos = firePoint.transform.position,
                TargetPos = firePoint.transform.position + shootDirection * hitScanWeaponData.ShootingRange,
                OnHitTarget = bullet => firePoint.ReturnBullet(bullet)
            };

            ShootBullet(hitData, bulletMoveTime, shootDirection);
        }

        private void ShootBullet(HitData hitData, float bulletMoveTime, Vector3 shootDirection)
        {
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

        public override float GetAttackRange()
        {
            return hitScanWeaponData.ShootingRange;
        }
    }
}
