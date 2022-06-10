using System.Collections.Generic;
using System.Linq;
using _ActionFarm.Scripts.Activities;
using _ActionFarm.Scripts.Components;
using _ActionFarm.Scripts.Utilities;
using UnityEngine;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroController : Singleton<HeroController>
    {
        [SerializeField] private VariableJoystick _floatingJoystick;

        [SerializeField] private HeroView _view;

        // [SerializeField] private HeroWeaponView _weapon;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private TriggerComponent _activityCheck;

        [Space] [Header("Activities")] [SerializeField]
        private List<ActivityBase> _currentActivities = new List<ActivityBase>();

        private float _traveledDistance;
        private bool _immune;
        private float turnSmoothVelocity;
        private float _rotationSmooth = .1f;

        private void Start() => _activityCheck.AddCallbacks(TryStartActivity, UnsetActivity);

        private void UnsetActivity(Collider other)
        {
            if (!other.TryGetComponent(out ActivityBase activity)) return;

            _currentActivities.Remove(activity);

            var firstGetResourcesActivity =
                _currentActivities.FirstOrDefault(x => x.ActivityType == ActivityType.GetResource);


            if (firstGetResourcesActivity == null)
            {
                _view.Attack(false);
            }
        }

        private void TryStartActivity(Collider other)
        {
            if (!other.TryGetComponent(out ActivityBase activity)) return;

            _currentActivities.Add(activity);

            if (activity.ActivityType == ActivityType.GetResource)
            {
                _view.Attack(true);
            }
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
    }
}