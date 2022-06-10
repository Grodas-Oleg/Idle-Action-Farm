using System;
using UnityEngine;

namespace _ActionFarm.Scripts.Activities
{
    public abstract class ActivityBase : MonoBehaviour
    {
        public abstract ActivityType ActivityType { get; protected set; }

        public virtual void Action()
        {
        }
    }

    [Serializable]
    public enum ActivityType
    {
        GetResource
    }
}