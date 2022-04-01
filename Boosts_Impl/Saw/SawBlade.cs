using HellWheels.Car;
using UnityEngine;

namespace HellWheels.Boosts.Saw
{
    public class SawBlade : MonoBehaviour
    {
        private ICar _racer;

        public bool Active { get; set; }

        public void Initialize(ICar selfRacer)
        {
            _racer = selfRacer;
        }

        private void OnTriggerEnter(Collider other)
        {
            var racerTag = other.GetComponent<RacerTag>();

            if (Active && racerTag != null && racerTag.Racer != null && racerTag.Racer != _racer)
            {
                racerTag.Racer.DieAndRespawnAfterSeconds(3.0f);
            }
        }
    }
}
