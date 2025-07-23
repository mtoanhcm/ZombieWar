using UnityEngine;

namespace ZombieWar.Character
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public Transform MainCharacter => mainCharacter.transform;

        [SerializeField]
        private MainCharacter mainCharacter;
        [SerializeField]
        private MainCharacterConfig mainCharacterConfig;

        private void Awake()
        {
            if (Instance == null) { 
                Instance = this;
            }   
        }

        private void Start()
        {
            mainCharacter.Spawn(new MainCharacterData(mainCharacterConfig));
        }
    }
}
