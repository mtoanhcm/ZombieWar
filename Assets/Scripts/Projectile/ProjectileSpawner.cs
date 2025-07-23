using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Projectile
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField]
        private ProjectileID bulletID;
        private ObjectPool<ProjectileBase> straightBulletPool;

        public void Start()
        {
            var bulletPrefab = Resources.Load<ProjectileBase>($"Projectile/{bulletID}");
            if (bulletPrefab == null)
            {
                DebugCustom.LogError($"Projectile prefab for ID {bulletID} not found in Resources/Projectile.");
                return;
            }

            straightBulletPool = new ObjectPool<ProjectileBase>(bulletPrefab, 20);
        }

        public T SpawnBullet<T>(bool isActiveBullet = true) where T : ProjectileBase
        {
            if (straightBulletPool == null)
            {
                DebugCustom.LogError("Projectile pool is not initialized.");
                return null;
            }

            var bullet = straightBulletPool.GetObject(isActiveBullet) as T;
            return bullet;
        }

        public void ReturnBullet(ProjectileBase bullet)
        {
            if (bullet == null)
            {
                DebugCustom.LogError("Cannot return a null bullet.");
                return;
            }

            straightBulletPool.ReturnObject(bullet);
            bullet.OnDestroy?.Invoke();
        }
    }
}
