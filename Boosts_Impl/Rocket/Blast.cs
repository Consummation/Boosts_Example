using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts.Rocket
{
    public class Blast : MonoBehaviour
    {
        [SerializeField] private AudioPlayer audioPlayer;
        [SerializeField] private ParticleSystem explosionPS;
        [SerializeField] private List<GameObject> visualsToDisable;

        public void Initialize(IAudioManager audioManager)
        {
            audioPlayer.SetAudioClips(audioManager);
        }

        public void Play()
        {
            explosionPS.Play();
            audioPlayer.PlaySound(AudioTypes.RocketPop);

            foreach (var v in visualsToDisable)
                v.SetActive(false);

            Destroy(gameObject, 2.0f);
        }
    }
}