using HellWheels.Car;
using UnityEngine;

namespace HellWheels.Boosts.Saw
{
    public class SawYawRotator : MonoBehaviour
    {
        private ICar _racer;

        public void Initialize(ICar selfRacer)
        {
            _racer = selfRacer;
        }

        public void RotateTowardsClosestRacer(ICar closestRacer)
        {
            var selfToClosest = (closestRacer.Transform.position - _racer.Transform.position).normalized;

            var angle = Vector3.SignedAngle(_racer.Transform.forward, selfToClosest, Vector3.up);
            var quat = Quaternion.Euler(0.0f, angle, 0.0f);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, quat, 10.0f);
        }
    }
}
