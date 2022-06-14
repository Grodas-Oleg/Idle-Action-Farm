using System;
using System.Collections;
using _ActionFarm.Scripts.Hero;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Material _material;
        [SerializeField] private Transform _target;
        [SerializeField] private Camera _uiCamera;

        [SerializeField] private float _force = 10f;
        [SerializeField] public ParticleSystem _particleSystem;

        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1];
        private Coroutine _currentCoroutine;

        private Vector3 _startPoint;
        private Vector3 _endPoint;


        private void Start()
        {
            var position = _startPoint = _uiCamera.ScreenToWorldPoint(_transform.position);
            var targetPosition = _endPoint = _uiCamera.ScreenToWorldPoint(_target.position);
            SpawnParticleToTarget(position, targetPosition, () => HeroController.Instance.AddCoins(1));
        }


        public void SpawnParticleToTarget(Vector3 particlePosition, Vector3 targetPosition, UnityAction action) =>
            _currentCoroutine = StartCoroutine(Spawn(particlePosition, targetPosition, action));

        private IEnumerator Spawn(Vector3 particlePosition, Vector3 targetPosition, UnityAction action)
        {
            while (enabled)
            {
                _particleSystem.GetComponent<ParticleSystemRenderer>().material = _material;
                _particleSystem.GetParticles(_particles);
                for (int i = 0; i < _particles.Length; i++)
                {
                    _particles[i].position =
                        Vector3.Lerp(particlePosition, targetPosition, _force);

                    if (!(Vector3.Distance(_particles[i].position, targetPosition) < .1f)) continue;

                    _particles[i].remainingLifetime = 0;
                    action.Invoke();
                    StopCoroutine(_currentCoroutine);
                }

                _particleSystem.SetParticles(_particles, _particles.Length);

                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawLine(_startPoint, _endPoint);
        }
    }
}