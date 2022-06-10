using System.Collections.Generic;

namespace _ActionFarm.Scripts.Hero.HeroInventory
{
    public class InventoryModel
    {
        public readonly Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();
    }
}