using System.Collections;
using _ActionFarm.Scripts.Hero;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Material _material;
        [SerializeField] private Transform _target;

        [SerializeField] private float _force = 10f;
        [SerializeField] public ParticleSystem _particleSystem;

        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1];
        private Coroutine _currentCoroutine;


        private void Start()
        {
            var position = Camera.main.WorldToScreenPoint(_transform.position);
            var targetPosition = Camera.main.WorldToScreenPoint(_target.position);
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
                        Vector3.Lerp(particlePosition, targetPosition, Time.deltaTime * _force);

                    if (!(Vector3.Distance(_particles[i].position, targetPosition) < .1f)) continue;

                    // _particles[i].remainingLifetime = 0;
                    action.Invoke();
                    // StopCoroutine(_currentCoroutine);
                }

                _particleSystem.SetParticles(_particles, _particles.Length);

                yield return null;
            }
        }
    }
}