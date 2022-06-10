using UnityEngine;

namespace _ValiantLight.Scripts.Components
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
       

        private Vector3 _offset;
        private float _smoothSpeed = .1f;

        private void Start()
        {
            _offset = transform.position;
        }

        private void Update()
        {
            var desPosition = _targetTransform.position + _offset;
            var smoothPosition = Vector3.Lerp(transform.position, desPosition, _smoothSpeed);
            transform.position = smoothPosition;

            transform.LookAt(_targetTransform);
        }
    }
}