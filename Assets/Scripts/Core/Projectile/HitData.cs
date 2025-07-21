using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public class HitData
    {
        public float Damage;
        public Vector3 FirePos;
        public Vector3 TargetPos;
        public Action<ProjectileBase> OnHitTarget;
    }
}
