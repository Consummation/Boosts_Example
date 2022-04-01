using HellWheels.Car;
using ScriptableObjects;
using UnityEngine;

namespace HellWheels.Boosts.Oil
{
    public class OilPuddle : MonoBehaviour
    {
        [SerializeField] private CarStats _boostStats;
        [SerializeField] private float _lifetime;
        private ICar _selfRacer;

        public void Initialize(ICar selfRacer)
        {
            _selfRacer = selfRacer;
            Destroy(gameObject, _lifetime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var racerTag = other.GetComponent<RacerTag>();

            if (racerTag != null && racerTag.Racer != _selfRacer)
            {
                var oilDebuff = new OilDebuff(racerTag.Racer, 5.0f, _boostStats);
                
                var debuffContainer = racerTag.Racer.DebuffContainer;
                if (!debuffContainer.HasOil())
                {
                    debuffContainer.AddDebuff(oilDebuff);
                }
            }
        }
    }

}