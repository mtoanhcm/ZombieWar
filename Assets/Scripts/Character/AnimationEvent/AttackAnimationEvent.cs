using System;
using UnityEngine;

namespace ZombieWar.Character
{
    public class AttackAnimationEvent : MonoBehaviour
    {
        public Action OnHit;

        public void HitEvent() {
            OnHit?.Invoke();
        }
    }
}
