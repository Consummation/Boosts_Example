using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HellWheels.Boosts.Saw
{
    public class SawAnimator : MonoBehaviour
    {
        [SerializeField] private Rig _sawRig;
        [SerializeField] private Transform _sawTargetTransform;
        [SerializeField] private Transform _sawDestinationTransform;
        [SerializeField] private float _swingTime;

        private Action _onSwigEnded;

        public void Swig(Action onSwigEnded, Transform target)
        {
            _onSwigEnded = onSwigEnded;
            StartCoroutine(Swig(target));
        }

        private IEnumerator Swig(Transform target)
        {
            var countdownTime = 0f;
            while (countdownTime < _swingTime)
            {
                countdownTime += Time.deltaTime;
                _sawRig.weight += Time.deltaTime * (1 / _swingTime);
                _sawTargetTransform.position = target.position;

                yield return new WaitForEndOfFrame();
            }
            _sawTargetTransform.position = target.position;
            _onSwigEnded?.Invoke();
        }
    }

}