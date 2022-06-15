using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _ActionFarm.Scripts.Activities;
using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Hero.HeroInventory;
using _ActionFarm.Scripts.Utilities;
using UnityEngine;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroController : Singleton<HeroController>
    {
        [SerializeField] private HeroView _view;
        [SerializeField] private HeroWeaponView _weapon;
        [SerializeField] private VariableJoystick _floatingJoystick;
        [SerializeField] private TriggerComponent _activityCheck;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private IntStat _coins;

        [Space] [Header("Activities")] [SerializeField]
        private List<ActivityBase> _currentActivities = new List<ActivityBase>();

        private Coroutine _coroutine;
        public IntStat Coins => _coins;
        private void Start() => _activityCheck.AddCallbacks(TryStartActivity, UnsetActivity);

        private void UnsetActivity(Collider other)
        {
            if (!other.TryGetComponent(out ActivityBase activity)) return;

            _currentActivities.Remove(activity);

            var firstGetResourcesActivity =
                _currentActivities.FirstOrDefault(x => x.ActivityType == ActivityType.GetResource);

            var tradingActivity = _currentActivities.Find(x => x.ActivityType == ActivityType.Trade);


            if (firstGetResourcesActivity == null)
            {
                _weapon.SetWeaponView(false);
                _view.Attack(false);
            }
            else if (tradingActivity != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void TryStartActivity(Collider other)
        {
            if (!other.TryGetComponent(out ActivityBase activity)) return;

            _currentActivities.Add(activity);

            if (activity.ActivityType == ActivityType.GetResource)
            {
                _view.Attack(true);
                _weapon.SetWeaponView(true);
            }
            else if (activity.ActivityType == ActivityType.Trade)
            {
                foreach (var resource in Inventory.inventoryModel.Resources.Keys.Where(resource =>
                    Inventory.inventoryModel.Resources[resource] > 0 &&
                    Inventory.inventoryModel.Resources.ContainsKey(resource)))
                    _coroutine = StartCoroutine(TrySendResource(resource, activity.transform));
            }
        }

        private IEnumerator TrySendResource(ResourceType resource, Transform target)
        {
            while (Inventory.CountResource(resource) > 0 && Inventory.inventoryModel.Resources.ContainsKey(resource))
            {
                Inventory.RemoveResource(resource);
                EventBus.onResourceSendToTrade?.Invoke(resource, target.transform);

                yield return new WaitForSeconds(.5f);
            }
        }

        public void RemoveActivity(ActivityBase activity)
        {
            _currentActivities.Remove(activity);

            var firstGetResourcesActivity =
                _currentActivities.FirstOrDefault(x => x.ActivityType == ActivityType.GetResource);

            if (firstGetResourcesActivity != null) return;
            _view.Attack(false);
            _weapon.SetWeaponView(false);
        }

        private void Update()
        {
            _rigidbody.velocity = new Vector3(_floatingJoystick.Horizontal * _moveSpeed, _rigidbody.velocity.y,
                _floatingJoystick.Vertical * _moveSpeed);

            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                _view.Move(_rigidbody.velocity.magnitude);
                _view.RotateHero(_rigidbody.velocity);
            }
            else
            {
                _view.StopMove();
            }
        }

        public void AddCoins(int value)
        {
            var oldValue = _coins.current;
            _coins.Add(value);
            EventBus.onAddCoins?.Invoke(_coins.current, oldValue);
        }
    }
}