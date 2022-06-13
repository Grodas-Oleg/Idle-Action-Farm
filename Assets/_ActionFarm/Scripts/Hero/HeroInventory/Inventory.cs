using System;

namespace _ActionFarm.Scripts.Hero.HeroInventory
{
    public static class Inventory
    {
        public static InventoryModel inventoryModel = new InventoryModel();

        public static event Action<ResourceType> onInventoryChanged;

        public static void AddResource(ResourceType resourceType, int quantity = 1)
        {
            if (quantity <= 0) return;

            var isExist = inventoryModel.Resources.ContainsKey(resourceType);

            if (isExist)
            {
                inventoryModel.Resources[resourceType] += quantity;
            }
            else
            {
                inventoryModel.Resources.Add(resourceType, quantity);
            }

            onInventoryChanged?.Invoke(resourceType);
        }

        public static bool RemoveResource(ResourceType resourceType, int quantity = 1)
        {
            inventoryModel.Resources[resourceType] -= quantity;
            if (inventoryModel.Resources[resourceType] <= 0)
                inventoryModel.Resources.Remove(resourceType);

            onInventoryChanged?.Invoke(resourceType);

            return true;
        }

        public static bool IsFullStack(ResourceType resourceType) => CountResource(resourceType) >= 40;

        public static int CountResource(ResourceType resourceType) =>
            inventoryModel.Resources.TryGetValue(resourceType, out int quantity) ? quantity : 0;
    }
}