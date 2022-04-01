using UnityEngine;

namespace HellWheels.Boosts.Saw
{
    public class SawBladeRotator : MonoBehaviour
    {
        [SerializeField] private GameObject pivot;

        private float _spinSpeed;

        public void Initialize(float spinSpeed)
        {
            _spinSpeed = spinSpeed;
        }

        public void Spin(float deltaTime)
        {
            pivot.transform.Rotate(_spinSpeed * deltaTime * Vector3.left);
        }
    }
}
