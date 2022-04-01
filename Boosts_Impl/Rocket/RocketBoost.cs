using HellWheels.Car;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts.Rocket
{
    public class RocketBoost : MonoBehaviour, IBoost
    {
        [SerializeField] private AudioPlayer audioPlayer;
        [SerializeField] private RocketTargetDetector rocketTargetDetector;
        [SerializeField] private Blast rocketExplosion;
        
        [SerializeField] private List<ParticleSystem> startEffects;

        [SerializeField] private float _rocketSpeed;

        private bool _isLaunched;
        private ICar _selfRacer;
        private ICar _targetRacer;

        public BoostType BoostType => BoostType.Rocket;

        public ICar Target => _targetRacer;

        public void Initialize(IAudioManager audioManager, ICar selfRacer,
            List<ICar> allRacers)
        {
            if (audioManager == null)
                throw new ArgumentNullException(nameof(audioManager));
            if (allRacers == null)
                throw new ArgumentNullException(nameof(allRacers));
            _selfRacer = selfRacer;
            audioPlayer.SetAudioClips(audioManager);

            rocketTargetDetector.Initialize(selfRacer, allRacers);
            rocketExplosion.Initialize(audioManager);

            audioPlayer.PlaySound(AudioTypes.RocketFly);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLaunched)
                return;

            // Collision with road
            if (other.gameObject.layer == 11)
            {
                rocketExplosion.Play();
                Dispose();
            }

            var racerTag = other.GetComponent<RacerTag>();
            if (racerTag != null && racerTag.Racer != _selfRacer)
            {
                racerTag.Racer.DieAndRespawnAfterSeconds(3.0f);
                rocketExplosion.Play();
            }
        }

        private void FixedUpdate()
        {
            if (!_isLaunched)
                _targetRacer = rocketTargetDetector.GetClosestRacerInFrontOfSelf();
            else
            {
                if (_targetRacer == null)
                    _targetRacer = rocketTargetDetector.GetClosestRacerInFrontOfSelf();
                var vectorToTarget = (_targetRacer.Transform.position - transform.position).normalized;
                var delta = _rocketSpeed * Time.fixedDeltaTime * vectorToTarget;
                transform.SetPositionAndRotation(transform.position + delta, Quaternion.LookRotation(vectorToTarget));
            }
        }

        public void Use(Action onUsed)
        {
            if (_isLaunched)
                return;
            _isLaunched = true;
            transform.parent = null;
            foreach (var effect in startEffects)
                effect.Play();
            audioPlayer.PlaySound(AudioTypes.RocketFly);
            onUsed?.Invoke();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}