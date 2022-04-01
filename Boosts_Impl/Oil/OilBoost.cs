using HellWheels.Car;
using System;
using System.Collections;
using UnityEngine;

namespace HellWheels.Boosts.Oil
{
    public class OilBoost : MonoBehaviour, IBoost
    {
        [SerializeField] OilPuddle _oilPuddlePrefab;

        private ICar _selfRacer;
        private Transform _spawnTransform;

        public BoostType BoostType => BoostType.Oil;

        public ICar Target => null;

        public void Initialize(Transform spawnTransform, ICar selfRacer)
        {
            _selfRacer = selfRacer;
            _spawnTransform = spawnTransform ?? throw new ArgumentNullException(nameof(spawnTransform));
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void Use(Action onUsed)
        {
            var oil = Instantiate(_oilPuddlePrefab, _spawnTransform.position, _spawnTransform.rotation);
            oil.Initialize(_selfRacer);
            onUsed?.Invoke();
            Destroy(gameObject);
        }
    }
}
