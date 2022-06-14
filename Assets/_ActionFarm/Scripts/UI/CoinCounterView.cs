using _ActionFarm.Scripts.EventLayer;
using _ActionFarm.Scripts.Handler;
using _ActionFarm.Scripts.Hero;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _ActionFarm.Scripts.UI
{
    public class CoinCounterView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _quantityText;

        private void OnEnable()
        {
            _quantityText.SetText(HeroController.Instance.Coins.current.ToString());
            EventBus.onAddCoins += UpdateCoinCounterView;
        }

        private void UpdateCoinCounterView(int newValue, int oldValue)
        {
            var collectedValue = newValue - oldValue;
            var valueThresholdForSound = collectedValue < 10 ? 1 : collectedValue / 10;
            var tempCurrency = 0;
            var collectCoinAudioClip = AudioHelper.GetClip("collect-coin_3");

            DOTween.To(() => oldValue, x => oldValue = x, newValue, 1.5f)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    if (oldValue - tempCurrency >= valueThresholdForSound)
                    {
                        AudioHelper.PlaySound(collectCoinAudioClip, 0.85f, 1.15f);
                        tempCurrency = oldValue;
                    }

                    _quantityText.SetText(oldValue.ToString());
                });
        }
    }
}