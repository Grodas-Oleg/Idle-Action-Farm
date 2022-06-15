using _ActionFarm.Scripts.EventLayer;

namespace _ActionFarm.Scripts.Hero.HeroInventory
{
    public static class Inventory
    {
        public static InventoryModel inventoryModel = new InventoryModel();
        public const int stakSize = 40;

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

            EventBus.onInventoryChanged?.Invoke(resourceType);
        }

        public static bool RemoveResource(ResourceType resourceType, int quantity = 1)
        {
            inventoryModel.Resources[resourceType] -= quantity;
            if (inventoryModel.Resources[resourceType] <= 0)
                inventoryModel.Resources.Remove(resourceType);

            EventBus.onInventoryChanged?.Invoke(resourceType);

            return true;
        }

        public static bool IsFullStack(ResourceType resourceType) => CountResource(resourceType) >= stakSize;

        public static int CountResource(ResourceType resourceType) =>
            inventoryModel.Resources.TryGetValue(resourceType, out int quantity) ? quantity : 0;
    }
}