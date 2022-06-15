using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero.HeroInventory;
using DG.Tweening;
using UnityEngine;

namespace _ActionFarm.Scripts.Props
{
    public class GoParticleForWorldComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _resourcePrefab;
        private void Start() => EventBus.onResourceSendToTrade += Spawn;

        private void Spawn(ResourceType resource, Transform target)
        {
            var instance = Instantiate(_resourcePrefab, transform.position, Quaternion.identity);

            instance.transform.DOMove(target.position, 1f)
                .OnComplete(() =>
                {
                    EventBus.onResourceComeToTrade?.Invoke(resource);
                    Destroy(instance);
                });
        }
    }
}