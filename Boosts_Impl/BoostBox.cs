using HellWheels.Car.Components;
using System;
using UnityEngine;

namespace HellWheels.Boosts
{
    public class BoostBox : MonoBehaviour
    {
        private const float ROTATION_SPEED = 90.0f;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform questionMark;
        [SerializeField] private AudioPlayer audioPlayer;

        private bool _isUsed = false;

        public void Initialize(IAudioManager audioManager)
        {
            audioPlayer.SetAudioClips(audioManager);
        }

        private void Update()
        {
            transform.Rotate(ROTATION_SPEED * Time.deltaTime * Vector3.up, Space.World);
            questionMark.LookAt(UnityEngine.Camera.main.transform.position, Vector3.up);
        }

        private void OnTriggerEnter(Collider other)
        {
            var boostContainer = other.GetComponent<IBoostContainer>();

            if (boostContainer != null && !_isUsed)
            {
                var boosts = Enum.GetValues(typeof(BoostType));
                var boost = (BoostType)boosts.GetValue(UnityEngine.Random.Range(0, boosts.Length));

                boostContainer.AddBoost(boost);
                animator.SetTrigger("Collect");
                audioPlayer.PlaySound(AudioTypes.Boost);
                Destroy(gameObject, 0.5f);
                _isUsed = true;
            }
        }
    }

}