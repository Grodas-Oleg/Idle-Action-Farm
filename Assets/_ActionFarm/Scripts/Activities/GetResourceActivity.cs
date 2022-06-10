using System;
using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.Hero.HeroInventory;
using UnityEngine;

namespace _ActionFarm.Scripts.Activities
{
    public class GetResourceActivity : ActivityBase
    {
        public ResourceType resourceType;

        [SerializeField] private int _quantity;
        [SerializeField] private TriggerComponent _triggerEnter;
        [SerializeField] private SpawnParticleComponent _particleComponent;

        public override ActivityType ActivityType
        {
            get => ActivityType.GetResource;
            protected set => throw new NotImplementedException();
        }

        private void OnEnable() => _triggerEnter.AddCallbacks(Add);

        private void Add(Collider other)
        {
            _particleComponent._particleSystem.Play();
            _particleComponent.SpawnParticleToTarget(other.transform,
                () =>
                {
                    Inventory.AddResource(resourceType, _quantity);
                    _particleComponent._particleSystem.Stop();
                });
        }
    }
}