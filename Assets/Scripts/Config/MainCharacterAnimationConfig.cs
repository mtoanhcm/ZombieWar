using UnityEngine;

namespace ZombieWar.Config
{
    [CreateAssetMenu(fileName = "MainCharacterAnimationConfig", menuName = "Config/AnimationConfig/MainCharacterAnimationConfig", order = 1)]
    public class MainCharacterAnimationConfig : ScriptableObject
    {
        public AnimatorOverrideController RifleAnimationController => rifleAnimationController;

        [SerializeField]
        private AnimatorOverrideController rifleAnimationController;
    }
}
