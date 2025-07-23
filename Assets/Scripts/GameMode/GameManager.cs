using UnityEngine;
using ZombieWar.Character;
using ZombieWar.Core;

namespace ZombieWar.GameMode {
    public class GameManager : MonoBehaviour
    {
        public CharacterBase MainCharacter => mainCharacter;

        [SerializeField]
        private MainCharacter mainCharacter;
        [SerializeField]
        private MainCharacterConfig mainCharacterConfig;

        public void Start()
        {
            mainCharacter.Spawn(new MainCharacterData(mainCharacterConfig));
        }
    }
}
