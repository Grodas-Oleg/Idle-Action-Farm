using System.Collections;
using UnityEngine;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _stepDelay;
        [SerializeField] private AudioSource _audioSource;

        private Coroutine _currentCoroutine;

        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int AttackAnimation = Animator.StringToHash("Harvest");

        public void Move(float moveSpeed)
        {
            _animator.SetFloat(Velocity, moveSpeed, .1f, Time.deltaTime);

            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(PlayStepSound());
        }

        public void StopMove()
        {
            _animator.SetFloat(Velocity, 0);

            if (_currentCoroutine == null)
                return;

            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        public void RotateHero(Vector3 velocity)
        {
            if (velocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(velocity);
        }

        public void Attack(bool status) => _animator.SetBool(AttackAnimation, status);


        private IEnumerator PlayStepSound()
        {
            var waitSeconds = new WaitForSeconds(_stepDelay);
            while (true)
            {
                _audioSource.pitch = Random.Range(0.85f, 1.05f);
                _audioSource.Play();
                yield return waitSeconds;
            }
        }
    }
}