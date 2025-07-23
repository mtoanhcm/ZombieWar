using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        protected CharacterBaseData characterData;

        public GameObject Self => gameObject;

        public Action<CharacterBase> OnDeath { get; set; }

        public virtual void Spawn<T>(T characterData) where T : CharacterBaseData {
            this.characterData = characterData;
        }
    }
}
