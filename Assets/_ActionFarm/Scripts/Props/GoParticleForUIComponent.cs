using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero;
using DG.Tweening;
using UnityEngine;

namespace _ActionFarm.Scripts.Props
{
    public class GoParticleForUIComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private RectTransform _endPoin;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _UICamera;
        private void Start() => EventBus.onCoinSend += Spawn;

        private void Spawn(int value)
        {
            var startPointInUI = _UICamera.WorldToViewportPoint(_startPoint.position);

            var instance = Instantiate(_coinPrefab,
                new Vector3(startPointInUI.x, startPointInUI.y, 1),
                Quaternion.identity,
                _canvas.transform);

            instance.transform.DOMove(_endPoin.position, 1f)
                .OnComplete(() =>
                {
                    HeroController.Instance.AddCoins(value);
                    Destroy(instance);
                });
        }
    }
}