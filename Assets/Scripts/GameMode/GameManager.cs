using UnityEngine;
using ZombieWar.Character;

namespace ZombieWar.GameMode {
    public class GameManager : MonoBehaviour
    {
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
