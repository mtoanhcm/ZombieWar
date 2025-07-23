using System;
using System.Collections;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterCombat : MonoBehaviour, ICombat
    {
        public bool CanAttack => isAttack;
        public GameObject Self => gameObject;
        public IWeapon CurrentWeapon => currentWeapon;

        public Action<Vector3> OnFocusDirectionChanged;
        public Action<IWeapon> OnCurrentWeaponChanged;
        public Action<bool> OnAttackStateChanged;

        private bool isAttack;
        private Vector3 directionFocus;
        private IWeapon currentWeapon;


        public void SetWeapon(IWeapon weapon) {
            currentWeapon = weapon;
            currentWeapon.SetOwner(this);
            OnCurrentWeaponChanged?.Invoke(currentWeapon);
        }

        public void AttackByAuto(Vector2 direction) {

            StopAllCoroutines();

            isAttack = direction != Vector2.zero;
            if (currentWeapon == null || !isAttack) {
                directionFocus = Vector3.zero;
                currentWeapon.Attack(false);
                OnFocusDirectionChanged?.Invoke(directionFocus);
                return;
            }

            directionFocus = new Vector3(direction.x, 0, direction.y);
            StartCoroutine(AutoAttack());
        }

        private IEnumerator AutoAttack() {
            while (true) {
                OnFocusDirectionChanged?.Invoke(directionFocus);

                if (!isAttack) {
                    break;
                }

                currentWeapon.Attack(true);

                yield return null;
            }

            currentWeapon.Attack(false);
        }

        public void AttackByCommand() {
            if (isAttack)
            {
                return;
            }

            isAttack = true;
            OnAttackStateChanged?.Invoke(isAttack);
            if (currentWeapon == null)
            {
                return;
            }

            currentWeapon.Attack(true);

            isAttack = false;
        }
    }
}
