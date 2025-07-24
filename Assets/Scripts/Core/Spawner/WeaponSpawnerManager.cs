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

        public void SpawnHitScanWeapon(WeaponBaseConfig config, out IWeapon weapon) {
            hitScanWeaponSpawner.SpawnWeapon(config, out weapon);
        }

        public void SpawnMeleeWeapon(WeaponBaseConfig config, out IWeapon weapon)
        {
            meleeWeaponSpawner.SpawnWeapon(config, out weapon);
        }
    }
}
