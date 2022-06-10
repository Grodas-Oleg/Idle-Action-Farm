using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.Components
{
    public class SpawnParticleComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _force = 10f;
        [SerializeField] public ParticleSystem _particleSystem;

        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1];
        private Coroutine _currentCoroutine;

        public void SpawnParticleToTarget(Transform targetPosition, UnityAction action)
        {
            _target = targetPosition;
            _currentCoroutine = StartCoroutine(Spawn(targetPosition, action));
        }

        private IEnumerator Spawn(Transform targetPosition, UnityAction action)
        {
            while (enabled)
            {
                _particleSystem.GetParticles(_particles);

                for (int i = 0; i < _particles.Length; i++)
                {
                    _particles[i].position =
                        Vector3.Lerp(_particles[i].position, targetPosition.position, Time.deltaTime * _force);

                    if (!(Vector3.Distance(_particles[i].position, targetPosition.position) < .1f)) continue;

                    _particles[i].remainingLifetime = 0;
                    action.Invoke();
                    StopCoroutine(_currentCoroutine);
                }

                _particleSystem.SetParticles(_particles, _particles.Length);

                yield return null;
            }
        }
    }
}