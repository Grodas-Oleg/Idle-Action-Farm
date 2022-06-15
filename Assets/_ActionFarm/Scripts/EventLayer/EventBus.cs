using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction<ResourceType, Transform> onResourceSendToTrade;
        public static UnityAction<ResourceType> onResourceComeToTrade;
        public static UnityAction<ResourceType> onInventoryChanged;
        public static UnityAction<int> onCoinSend;
        public static UnityAction<int, int> onAddCoins;

        static EventBus()
        {
        }

        public static void Clear()
        {
        }
    }
}