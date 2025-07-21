using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacterCombat : MonoBehaviour, ICombat
    {
        public bool IsAttackin() => isAttacking;
        public Action<Vector3> OnFocusDirectionChanged;
        public Action<IWeapon> OnCurrentWeaponChanged;

        private bool isAttacking;
        private Vector3 directionFocus;
        private IWeapon currentWeapon;

        public GameObject Self => gameObject;

        private void Update()
        {
            OnFocusDirectionChanged?.Invoke(directionFocus);
            if (!isAttacking) {
                return;
            }

            currentWeapon.Attack();
        }

        public void SetWeapon(IWeapon weapon) {
            currentWeapon = weapon;
            currentWeapon.SetOwner(this);
            OnCurrentWeaponChanged?.Invoke(currentWeapon);
        }

        public void OnAttackByLook(Vector2 direction) {
            isAttacking = direction != Vector2.zero;
            if (currentWeapon == null || !isAttacking) {
                directionFocus = Vector3.zero;
                return;
            }

            directionFocus = new Vector3(direction.x, 0, direction.y);
        }
    }
}
