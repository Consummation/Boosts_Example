using HellWheels.Car;
using ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;

namespace HellWheels.Boosts.SpeedUp
{
    public class SpeedUpBoost : MonoBehaviour, IBoost
    {
        [SerializeField] private GameObject airEffect;
        [SerializeField] private ParticleSystem jetFlames;

        [SerializeField] private SpeedUpCamera speedUpCamera;
        [SerializeField] private CarStats _boostStats;
        [SerializeField] private float _duration;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _disconnectForce;
        [SerializeField] private float _disconnectLifetime;

        private bool _isUsed;

        private CarSettings _carSettings;

        public BoostType BoostType => BoostType.SpeedUp;

        public ICar Target => null;

        public void Initialize(ICar selfRacer, IRacingCamera racingCamera)
        {
            speedUpCamera.Initialize(racingCamera);

            _carSettings = selfRacer.CarSettings;

            airEffect.SetActive(false);
            jetFlames.gameObject.SetActive(false);
        }

        private IEnumerator BoostSpeed(Action onEnd)
        {
            _carSettings.CurrentStats += _boostStats;
            speedUpCamera.SetManualFov();

            airEffect.SetActive(true);
            jetFlames.gameObject.SetActive(true);
            jetFlames.Play();
            _isUsed = true;

            yield return new WaitForSeconds(_duration);

            _carSettings.CurrentStats -= _boostStats;
            speedUpCamera.SetDefaultFov();
            airEffect.SetActive(false);
            jetFlames.gameObject.SetActive(false);
            DisconnectFromCar();
            onEnd?.Invoke();
        }

        private void DisconnectFromCar()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.up, ForceMode.Impulse);
            transform.parent = null;
            Destroy(gameObject, _disconnectLifetime);
        }

        public void Use(Action onUsed)
        {
            if (_isUsed)
                return;
            StartCoroutine(BoostSpeed(onUsed));
        }

        public void Dispose()
        {
            if (_isUsed)
                _carSettings.CurrentStats -= _boostStats;
            Destroy(gameObject);
        }
    }
}

