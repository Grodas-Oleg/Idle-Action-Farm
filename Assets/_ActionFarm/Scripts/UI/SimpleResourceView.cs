using System;
using System.Linq;
using _ActionFarm.Scripts.Hero.HeroInventory;
using TMPro;
using UnityEngine;

namespace _ActionFarm.Scripts.UI
{
    public class SimpleResourceView : BaseView
    {
        [SerializeField] private ResourceDataGroup[] _resourceDatas;

        private void OnEnable()
        {
            foreach (var resourceData in _resourceDatas)
            {
                resourceData.quantityText.text = Inventory.inventoryModel.Resources.ContainsKey(resourceData.resource)
                    ? "x " + Inventory.inventoryModel.Resources[resourceData.resource]
                    : "0";
            }

            Inventory.onInventoryChanged += UpdateView;
        }

        private void UpdateView(ResourceType resource)
        {
            var resourceData = _resourceDatas.FirstOrDefault(x => x.resource == resource);
            if (resourceData == null)
            {
                Debug.LogError($"ResourceType {resource} not found");
                return;
            }

            Inventory.inventoryModel.Resources.TryGetValue(resource, out int currentQuantity);
            resourceData.quantityText.text = "x " + currentQuantity;
        }
    }

    [Serializable]
    public class ResourceDataGroup
    {
        public ResourceType resource;
        public TextMeshProUGUI quantityText;
    }
}