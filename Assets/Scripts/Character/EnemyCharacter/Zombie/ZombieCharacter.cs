using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class ZombieCharacter : CharacterBase<SimpleEnemyData>
    {
        public override void Spawn(SimpleEnemyData characterData)
        {
            base.Spawn(characterData);

        }
    }
}
