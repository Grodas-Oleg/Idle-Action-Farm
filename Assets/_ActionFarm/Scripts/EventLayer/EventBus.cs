using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction<ResourceType> onResourceSend;
        public static UnityAction<int, int> onAddCoins;

        static EventBus()
        {
        }

        public static void Clear()
        {
        }
    }
}