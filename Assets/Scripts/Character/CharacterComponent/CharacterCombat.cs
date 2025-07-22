using System;
using System.Collections;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterCombat : MonoBehaviour, ICombat
    {
        public bool IsAttackin() => isAttacking;
        public Action<Vector3> OnFocusDirectionChanged;
        public Action<IWeapon> OnCurrentWeaponChanged;
        public Action<bool> OnAttackStateChanged;

        private bool isAttacking;
        private Vector3 directionFocus;
        private IWeapon currentWeapon;

        public GameObject Self => gameObject;

        private void Update()
        {
            
        }

        public void SetWeapon(IWeapon weapon) {
            currentWeapon = weapon;
            currentWeapon.SetOwner(this);
            OnCurrentWeaponChanged?.Invoke(currentWeapon);
        }

        public void OnAttackByLook(Vector2 direction) {

            StopAllCoroutines();

            isAttacking = direction != Vector2.zero;
            if (currentWeapon == null || !isAttacking) {
                directionFocus = Vector3.zero;
                return;
            }

            directionFocus = new Vector3(direction.x, 0, direction.y);
            StartCoroutine(AutoAttack());
        }

        private IEnumerator AutoAttack() {
            while (true) {
                OnFocusDirectionChanged?.Invoke(directionFocus);

                if (!isAttacking) {
                    break;
                }
                currentWeapon.Attack();

                yield return null;
            }
        }

        public void OnAttackByCommand(bool isAttack) { 
            isAttacking = isAttack;
            OnAttackStateChanged?.Invoke(isAttacking);

            if (currentWeapon == null || !isAttacking) {
                return;
            }

            currentWeapon.Attack();
        }
    }
}
