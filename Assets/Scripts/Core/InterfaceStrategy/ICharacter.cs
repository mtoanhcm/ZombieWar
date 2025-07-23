using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public interface ICharacter
    {
        Action<CharacterBase> OnDeath { get; set; }
        GameObject Self { get; }
    }
}
