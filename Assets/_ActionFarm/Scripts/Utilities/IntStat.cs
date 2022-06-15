using System;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.Utilities
{
    [Serializable]
    public class IntStat
    {
        public int max;
        public int current;
        public event UnityAction<int, int> OnChange;
        public bool IsMax => current >= max;

        public IntStat(int max = default, int current = default)
        {
            this.max = max;
            this.current = current;
        }

        #region Getters

        public int GetMax => max;
        public int GetCurrent => current;

        #endregion

        public void SetMax(int value, bool resetCurrentToMax = true)
        {
            max = value;
            if (resetCurrentToMax)
                current = value;
            InvokeChangeEvent();
        }

        public void SetCurrentToMax()
        {
            current = max;
            InvokeChangeEvent();
        }

        public void Set(int value)
        {
            current = value;
            InvokeChangeEvent();
        }

        public void Add(int value)
        {
            current += value;
            current = Mathf.Clamp(current, 0, max);
            InvokeChangeEvent();
        }

        protected virtual void InvokeChangeEvent()
		{
            OnChange?.Invoke(max, current);
        }

        public IntStat GetClone()
        {
            return new IntStat(max, current);
        }
    }
}