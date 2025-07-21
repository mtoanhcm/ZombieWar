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
            if (bulletPrefab == null) { 
                DebugCustom.LogError($"Projectile prefab for ID {bulletID} not found in Resources/Projectile.");
                return;
            }

            straightBulletPool = new ObjectPool<ProjectileBase>(bulletPrefab, 10, transform);
        }

        public T SpawnBullet<T>() where T : ProjectileBase
        {
            if (straightBulletPool == null) {
                DebugCustom.LogError("Projectile pool is not initialized.");
                return null;
            }

            var bullet = straightBulletPool.GetObject() as T;

            bullet.OnDestroy += () =>
            {
                bullet.OnDestroy = null;
                straightBulletPool.ReturnObject(bullet);
            };

            return bullet;
        }
    }
}
