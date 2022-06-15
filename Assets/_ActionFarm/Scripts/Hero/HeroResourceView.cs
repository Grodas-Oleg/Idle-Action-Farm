using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroResourceView : MonoBehaviour
    {
        [SerializeField] private GameObject _resourceView;

        private void Start() => EventBus.onInventoryChanged += resourceType =>
            _resourceView.SetActive(Inventory.inventoryModel.Resources.ContainsKey(resourceType));
    }
}