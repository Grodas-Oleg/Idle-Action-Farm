using System;
using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero;
using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine;

namespace _ActionFarm.Scripts.Activities
{
    public class ResourceTradeActivity : ActivityBase
    {
        // [SerializeField] private TriggerComponent _enterTrigger;

        [SerializeField] private ResourcesTradeRatio[] _resourcesTradeRatio;
        [SerializeField] private SpawnParticleComponent _particleComponent;
        [SerializeField] private RectTransform _UI;

        public override ActivityType ActivityType
        {
            get => ActivityType.Trade;
            protected set => throw new NotImplementedException();
        }

        private void OnEnable()
        {
            EventBus.onResourceSend += TradeResource;
        }

        private void TradeResource(ResourceType resource)
        {
            for (var i = 0; i < _resourcesTradeRatio.Length; i++)
            {
                if (_resourcesTradeRatio[i].resource == resource)
                {
                    // HeroController.Instance.AddCoins(_resourcesTradeRatio[i].resourceCost);
                    // var Position = Camera.main.WorldToScreenPoint(transform.position);
                    // var UiPosition = Camera.main.WorldToViewportPoint(_UI.position);
                    _particleComponent.SpawnParticleToTarget(_UI.transform.position,
                        () => HeroController.Instance.AddCoins(_resourcesTradeRatio[i].resourceCost));
                }
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