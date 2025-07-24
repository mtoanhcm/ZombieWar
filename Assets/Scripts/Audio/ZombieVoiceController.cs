using System.Threading.Tasks;
using UnityEngine;

namespace ZombieWar.Audio
{
    //HARDCODE
    public class ZombieVoiceController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource[] voices;

        private async void Start()
        {
            await Task.Delay(3000);

            foreach (var voice in voices) {
                voice.Play();
                await Task.Delay(Random.Range(3, 7) * 1000);
            }
        }
    }
}
