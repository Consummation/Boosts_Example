using HellWheels.Car;
using UnityEngine;

namespace HellWheels.Boosts.SpeedUp
{
    public class SpeedUpCamera : MonoBehaviour
    {
        private IRacingCamera _racingCamera;
        private float _initialFov;

        [SerializeField] private float _fovScaleFactor;

        public void Initialize(IRacingCamera racingCamera)
        {
            if (racingCamera != null)
            {
                _racingCamera = racingCamera;
                _initialFov = _racingCamera.CameraComponent.fieldOfView;
            }
        }

        public void SetManualFov()
        {
            if (_racingCamera == null)
                return;

            var newFieldOfView = _initialFov * _fovScaleFactor;

            _racingCamera.ManualFov = true;
            _racingCamera.CameraComponent.fieldOfView += newFieldOfView * 0.1f;
        }

        public void SetDefaultFov()
        {
            if (_racingCamera == null)
                return;

            _racingCamera.ManualFov = false;
        }
    }
}
