using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Projectile;

namespace ZombieWar.Spawner
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField]
        private StraightBulletProjectile strightBulletPrefab;

        private ObjectPool<ProjectileBase> straightBulletPool;

        public void Start()
        {
            straightBulletPool = new ObjectPool<ProjectileBase>(strightBulletPrefab, 10, transform);
        }

        public T SpawnBullet<T>(ProjectileID bulletID) where T : ProjectileBase
        {
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
