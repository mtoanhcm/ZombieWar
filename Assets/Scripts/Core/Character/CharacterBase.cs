using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        protected CharacterBaseData characterData;

        public GameObject Self => gameObject;

        public virtual void Spawn<T>(T characterData) where T : CharacterBaseData {
            this.characterData = characterData;
        }
    }
}
