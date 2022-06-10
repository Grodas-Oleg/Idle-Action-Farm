using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.Components
{
    public class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private bool _isDisableOnEnter;
        [SerializeField] private bool _isDisableOnExit;

        [SerializeField] private UnityEvent<Collider> _onEnter;
        [SerializeField] private UnityEvent<Collider> _onExit;

        public void AddCallbacks(UnityAction<Collider> onEnter = null, UnityAction<Collider> onExit = null)
        {
            if (_onEnter != null)
                _onEnter.AddListener(onEnter);

            if (onExit != null)
                _onExit.AddListener(onExit);
        }

        private void OnTriggerEnter(Collider other)
        {
            _onEnter?.Invoke(other);
            if (_isDisableOnEnter)
                gameObject.SetActive(false);
        }

        private void OnTriggerExit(Collider other)
        {
            _onExit?.Invoke(other);
            if (_isDisableOnExit)
                gameObject.SetActive(false);
        }
    }
}