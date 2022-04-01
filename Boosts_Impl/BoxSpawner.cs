using UnityEngine;
using Zenject;

namespace HellWheels.Boosts
{
    public class BoxSpawner : MonoBehaviour
    {
        [SerializeField] private BoostBox boxPrefabTemplate;

        [Space(20)] [Range(0.0f, 2.0f)] public float _offsetFromGround = 0.3f;
        public float _timeToNextBoxSpawned = 5.0f;

        private IAudioManager _audioManager;
        private BoostBox _currentBoxInstance;
        private float _time;
        private bool _isInitialized;

        private void Update()
        {
            if (!_isInitialized)
                return;
            if (_currentBoxInstance != null)
                return;

            if (_time < _timeToNextBoxSpawned)
            {
                _time += Time.deltaTime;
            }
            else
            {
                _time = 0;
                SpawnBox();
            }
        }

        [Inject]
        public void Initialize(IAudioManager audioManager)
        {
            _isInitialized = true;
            _audioManager = audioManager;
            SpawnBox();
        }

        private void SpawnBox()
        {
            _currentBoxInstance = Instantiate(boxPrefabTemplate, transform.position + Vector3.up * _offsetFromGround, Quaternion.Euler(45.0f, 45.0f, 0.0f));
            _currentBoxInstance.Initialize(_audioManager);
            _currentBoxInstance.transform.parent = transform;
        }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            var oldColor = Gizmos.color;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.05f);

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position + Vector3.up * _offsetFromGround, Vector3.one * 0.4f);

            Gizmos.color = oldColor;
        }

#endif
    }

}