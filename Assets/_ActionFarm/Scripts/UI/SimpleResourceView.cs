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
                resourceData.quantityText.SetText(Inventory.inventoryModel.Resources.ContainsKey(resourceData.resource)
                    ? Inventory.inventoryModel.Resources[resourceData.resource] + "/40"
                    : "0/40");
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
            resourceData.quantityText.SetText(currentQuantity + "/40");
        }
    }

    [Serializable]
    public class ResourceDataGroup
    {
        public ResourceType resource;
        public TextMeshProUGUI quantityText;
    }
}