using System;
using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine;

namespace _ActionFarm.Scripts.Activities
{
    public class ResourceTradeActivity : ActivityBase
    {
        [SerializeField] private ResourcesTradeRatio[] _resourcesTradeRaito;

        public override ActivityType ActivityType
        {
            get => ActivityType.Trade;
            protected set => throw new NotImplementedException();
        }

        private void OnEnable() => EventBus.onResourceComeToTrade += TradeResource;


        private void TradeResource(ResourceType resource)
        {
            foreach (var resourceRaito in _resourcesTradeRaito)
            {
                if (resourceRaito.resource == resource) EventBus.onCoinSend?.Invoke(resourceRaito.resourceCost);
            }
        }
    }

    [Serializable]
    public class ResourcesTradeRatio
    {
        public ResourceType resource;
        public int resourceCost;
    }
}