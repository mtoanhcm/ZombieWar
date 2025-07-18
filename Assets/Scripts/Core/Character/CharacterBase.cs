using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBase<T> : MonoBehaviour where T : CharacterBaseData
    {
        protected T characterData;

        public virtual void Spawn(T characterData) {
            this.characterData = characterData;
        }
    }
}
