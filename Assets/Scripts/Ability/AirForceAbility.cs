using System.Threading.Tasks;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Ability
{
    public class AirForceAbility : AbilityBase
    {
        //Hard code ALL

        [Header("Attribute")]
        public float ExplosionRadius = 5f;
        public float ExplosionForceHorizontal = 5f;
        public float ExplostionForceVertical = 1f;
        public float Damage = 500;
        public LayerMask AffectedLayers;

        [Header("VFX")]
        public GameObject SmokeReadyAirForceVFX;
        public GameObject ExploseVFX;

        public async override void ActiveAbility(Vector3 position)
        {
            PrepareAbility();

            await Task.Delay(5000);

            CallExploseVFX();
            DoAbility();

            return;

            async void PrepareAbility() {
                var smoke = Instantiate(SmokeReadyAirForceVFX);
                smoke.transform.position = position;

                await Task.Delay(5000);

                Destroy(smoke);
            }

            async void CallExploseVFX()
            {
                var explose = Instantiate(ExploseVFX);
                explose.transform.position = position;

                await Task.Delay(3000);

                Destroy(explose);
            }

            void DoAbility() {
                Collider[] colliders = Physics.OverlapSphere(position, ExplosionRadius, AffectedLayers);

                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent<IHealth>(out var health))
                    {
                        health.TakeDamage(Damage);
                    }

                    Rigidbody rb = collider.attachedRigidbody;
                    if (rb != null)
                    {
                        Vector3 direction = (rb.position - position).normalized + Vector3.up * ExplostionForceVertical;
                        rb.AddForce(direction * ExplosionForceHorizontal, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
