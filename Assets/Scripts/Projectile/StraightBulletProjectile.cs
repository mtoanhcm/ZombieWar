using System;
using System.Collections;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Projectile
{
    public class StraightBulletProjectile : ProjectileBase
    {
        private Vector3 direction;
        private Vector3 hitPos;
        private float travelTime;
        private Action hitAction;


        public override void Spawn(float moveTime, HitData hitData)
        {
            base.Spawn(moveTime, hitData);

            transform.LookAt(hitData.TargetPos);

            direction = (hitData.TargetPos - hitData.FirePos).normalized;

            if(Physics.Raycast(hitData.FirePos, direction, out RaycastHit hitInfo, float.MaxValue, hitData.TargetLayer))
            {
                hitPos = hitInfo.point;
                travelTime = 0;
                canMove = true;
            }
        }

        protected override void MoveToTarget()
        {
            travelTime += Time.deltaTime;
            float travelFraction = travelTime / moveTime;

            Vector3 newPos;

            if (travelFraction >= 1)
            {
                newPos = hitPos;
                canMove = false;
                hitAction?.Invoke();
            }
            else { 
                newPos = Vector3.Lerp(hitData.FirePos, hitPos, travelFraction);
            }

            transform.position = newPos;
        }
    }
}
