using System;
using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.Hero;
using Unity.Mathematics;
using UnityEngine;

namespace _ActionFarm.Scripts.Activities
{
    public class GetResourceActivity : ActivityBase
    {
        [SerializeField] private TriggerComponent _triggerEnter;
        [SerializeField] private PlantGrowthController _plantController;
        [SerializeField] private GameObject _prefab;

        public override ActivityType ActivityType
        {
            get => ActivityType.GetResource;
            protected set => throw new NotImplementedException();
        }

        private void OnEnable() => _triggerEnter.AddCallbacks(Add);

        private void Add(Collider other)
        {
            _plantController.HarvestPlant(() => HeroController.Instance.RemoveActivity(this));
            var instance = Instantiate(_prefab, transform.position, quaternion.identity);
            instance.GetComponent<Rigidbody>()
                .AddForce(Vector3.one, ForceMode.Impulse);
        }
    }
}