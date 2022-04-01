using HellWheels.Car;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts.Saw
{
    public class SawBoost : MonoBehaviour, IBoost
    {
        [SerializeField] private SawAnimator sawAnimator;
        [SerializeField] private SawBlade sawBlade;
        [SerializeField] private SawBladeRotator sawBladeRotator;
        [SerializeField] private SawYawRotator sawYawRotator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        [SerializeField] private float _spinSpeed;
        [SerializeField] private float _brokenSawLifetime;
        [SerializeField] private float _disconnectForce;

        private ICar _closestRacer;
        private ICar _selfRacer;
        private List<ICar> _allRacers;

        private bool _isUsed;
        private bool _isUsing;

        public BoostType BoostType => BoostType.Saw;

        public ICar Target => null;

        public void Initialize(ICar selfRacer, List<ICar> allRacers)
        {
            _selfRacer = selfRacer;
            _allRacers = allRacers;
            sawBlade.Initialize(selfRacer);
            sawBladeRotator.Initialize(_spinSpeed);
            sawYawRotator.Initialize(selfRacer);
        }

        private void Update()
        {
            if (!_isUsed)
            {
                _closestRacer = GetClosestRacer();
                sawYawRotator.RotateTowardsClosestRacer(_closestRacer);
                sawBladeRotator.Spin(Time.deltaTime);
            }
        }

        public void Use(Action onUsed)
        {
            sawBlade.Active = true;

            if (_isUsed || _isUsing)
                return;
            _isUsing = true;
            sawAnimator.Swig(() =>
            {
                transform.SetParent(null);
                sawBlade.Active = false;
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(transform.up * _disconnectForce, ForceMode.Impulse);
                _collider.enabled = true;
                _isUsing = false;
                _isUsed = true;
                onUsed?.Invoke();
                Destroy(gameObject, _brokenSawLifetime);
            }, _closestRacer.Transform);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private ICar GetClosestRacer()
        {
            var index = 0;
            var closestDist = Mathf.Infinity;

            for (int i = 0; i < _allRacers.Count; i++)
            {
                if (_allRacers[i] == _selfRacer || !_allRacers[i].IsAlive)
                    continue;

                var dist = Vector3.Distance(transform.position, _allRacers[i].Transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    index = i;
                }
            }

            return _allRacers[index];
        }
    }
}