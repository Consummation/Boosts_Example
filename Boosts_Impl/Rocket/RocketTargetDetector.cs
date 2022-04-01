using HellWheels.Car;
using System.Collections.Generic;
using UnityEngine;

namespace HellWheels.Boosts.Rocket
{
    public class RocketTargetDetector : MonoBehaviour
    {
        private ICar _selfRacer;
        private IList<ICar> _racers;

        public void Initialize(ICar boostOwner, List<ICar> racers)
        {
            _selfRacer = boostOwner;
            _racers = racers;
        }

        public ICar GetClosestRacerInFrontOfSelf()
        {
            var index = -1;
            var closestDist = Mathf.Infinity;

            for (int i = 0; i < _racers.Count; i++)
            {
                if (_racers[i] == _selfRacer)
                    continue;

                if (_racers[i].LapInfo.CurrentWaypointIndex <= _selfRacer.LapInfo.CurrentWaypointIndex)
                    continue;

                if (!_racers[i].IsAlive)
                    continue;

                var dist = Vector3.Distance(_selfRacer.Transform.position, _racers[i].Transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    index = i;
                }
            }

            if (index > -1)
            {
                return _racers[index];
            }
            else
            {
                // If there is no racers in front of this racer then return previous one from the list of racers
                var indexOf = _racers.IndexOf(_selfRacer);
                indexOf--;
                if (indexOf < 0)
                    indexOf = _racers.Count - 1;

                return _racers[indexOf];
            }
        }
    }
}