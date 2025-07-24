using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterAbilityHolder : MonoBehaviour
    {
        //Hard code
        [SerializeField]
        private AbilityBase ability;
        [SerializeField]
        private float coolDown;

        private float tempCoolDownTime;

        public void ActiveAbility(Vector3 position)
        {
            if(tempCoolDownTime > Time.time)
            {
                DebugCustom.Log($"Your ability is not ready, please wait in a short time");
                return;
            }

            tempCoolDownTime = coolDown + Time.time;

            ability.ActiveAbility(position);
        }
    }
}
