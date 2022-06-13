using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.Handler;
using _ActionFarm.Scripts.Hero.HeroInventory;
using DG.Tweening;
using UnityEngine;

namespace _ActionFarm.Scripts.Props
{
    public class ResourcePickUp : MonoBehaviour
    {
        [SerializeField] private ResourceType _resource;
        [SerializeField] private TriggerComponent _trigger;

        private void OnEnable() => _trigger.AddCallbacks(PickUp);

        private void PickUp(Collider other)
        {
            if (!Inventory.IsFullStack(_resource))
                transform.DOMove(other.transform.position, .3f)
                    .OnComplete(() =>
                    {
                        Inventory.AddResource(_resource);
                        AudioHelper.PlaySoundByName("collect-coin_1");
                        Destroy(gameObject);
                    });
        }
    }
}