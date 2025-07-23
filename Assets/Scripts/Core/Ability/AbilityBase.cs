using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class AbilityBase : MonoBehaviour
    {
        public abstract void ActiveAbility(Vector3 position);
    }
}
