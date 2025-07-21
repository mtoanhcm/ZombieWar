using UnityEngine;

namespace ZombieWar.Weapon
{
    public class MagazineAttachment
    {
        public bool HasAmmo => CurrentAmmo > 0;
        public int MaxAmmoCapacity { get; private set; }
        public int CurrentAmmo { get; private set; }

        public MagazineAttachment(int maxAmmoCapicity) {
            MaxAmmoCapacity = maxAmmoCapicity;
            CurrentAmmo = maxAmmoCapicity;
        }

        public void UseAmmo(int amount)
        {
            if (amount <= 0) {
                return;
            }

            CurrentAmmo = Mathf.Max(CurrentAmmo - amount, 0);
        }

        public void AddAmmo(int amount)
        {
            if (amount <= 0) {
                return;
            }

            CurrentAmmo = Mathf.Min(CurrentAmmo + amount, MaxAmmoCapacity);
        }
    }
}
