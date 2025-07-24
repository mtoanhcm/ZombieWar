using System;
using System.Collections;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Projectile
{
    public class StraightBulletProjectile : ProjectileBase
    {
        private Vector3 hitPos;
        private float travelTime;


        public override void Spawn(float moveTime, HitData hitData)
        {
            base.Spawn(moveTime, hitData);

            transform.LookAt(hitData.TargetPos);
            transform.position = hitData.FirePos;

            hitPos = hitData.TargetPos;

            travelTime = 0;

            gameObject.SetActive(true);

            canMove = true;
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
                hitData.OnHitTarget?.Invoke(this);
                return;
            }
            else { 
                newPos = Vector3.Lerp(hitData.FirePos, hitPos, travelFraction);
            }

            transform.position = newPos;
        }
    }
}
