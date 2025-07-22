using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Weapon
{
    public class MeleeWeapon : WeaponBase
    {
        private MeleeWeaponData meleeWeaponData;
        private LayerMask targetLayerMask;
        private float tempAttackDelayTime;
        private Collider[] hits;

        public override void Spawn<T>(T weaponData)
        {
            base.Spawn(weaponData);

            meleeWeaponData = weaponData as MeleeWeaponData;
        }

        public override bool AddAttachment()
        {
            return false;
        }

        public override void Attack()
        {
            if(tempAttackDelayTime > Time.time)
            {
                return;
            }

            ApplyConeDamage();

            tempAttackDelayTime = Time.time + meleeWeaponData.Cooldown;
        }

        public override void SetOwner(ICombat owner)
        {
            this.owner = owner;
            targetLayerMask = ObjectLayer.TargetHitLayer(LayerMask.LayerToName(owner.Self.layer));
        }

        public void SnapToHandGrabPoint(Transform grabPoint) { 
        
        }

        private void ApplyConeDamage() {
            var ownerTrans = owner.Self.transform;
            if (Physics.OverlapSphereNonAlloc(ownerTrans.position, meleeWeaponData.AttackRange, hits, targetLayerMask) < 0) {
                return;
            }

            float cosHalfAngle = Mathf.Cos(meleeWeaponData.AttackAngle * 0.5f * Mathf.Deg2Rad);
            foreach (var hit in hits) {
                if(!hit.gameObject.TryGetComponent(out IHealth health))
                {
                    continue;
                }

                var toTarget = (hit.transform.position - ownerTrans.position).normalized;
                var dot = Vector3.Dot(ownerTrans.forward, toTarget);

                if (dot >= cosHalfAngle) {
                    health.TakeDamage(meleeWeaponData.Damage);
                }
            }
        }
    }
}
