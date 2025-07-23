using UnityEngine;

namespace ZombieWar.Core
{
    public class WeaponSpawnerManager : MonoBehaviour
    {
        public static WeaponSpawnerManager Instance { get; private set; }

        [SerializeField]
        private WeaponSpawnerBase hitScanWeaponSpawner;
        [SerializeField]
        private WeaponSpawnerBase meleeWeaponSpawner;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void SpawnHitScanWeapon(WeaponBaseConfig config, out IWeapon weapon, Transform grabHand) {
            hitScanWeaponSpawner.SpawnWeapon(config, out weapon, grabHand);
        }

        public void SpawnMeleeWeapon(WeaponBaseConfig config, out IWeapon weapon, Transform grabHand)
        {
            meleeWeaponSpawner.SpawnWeapon(config, out weapon, grabHand);
        }
    }
}
