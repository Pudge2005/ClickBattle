using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Earners
{
    public sealed class EarnerUi : MonoBehaviour
    {
        private const int _lvlsAmount = 1;


        [SerializeField] private IEarner _earner;

        [SerializeField] private Button _buyPositiveLevelsButton;
        [SerializeField] private Button _sellPositiveLevelsButton;

        [SerializeField] private Button _buyNegativeLevelsButton;
        [SerializeField] private Button _sellNegativeLevelsButton;


        private void Awake()
        {
            GameManager.BalanceChanged += HandleBalanceChanged;
            UpdateButtonsInteractivityState(GameManager.Balance);
        }


        private void Start()
        {
            _buyPositiveLevelsButton.onClick.AddListener(BuyPositiveLevels);
            _sellPositiveLevelsButton.onClick.AddListener(SellPositiveLevels);
            _buyNegativeLevelsButton.onClick.AddListener(BuyNegativeLevels);
            _sellNegativeLevelsButton.onClick.AddListener(SellNegativeLevels);
        }


        private void HandleBalanceChanged(GameManager gameManager, float newBalance, float delta)
        {
            UpdateButtonsInteractivityState(newBalance);
        }

        private void UpdateButtonsInteractivityState(float newBalance)
        {
            //check can buy/sell
        }


        private void BuyPositiveLevels()
        {
            _ = _earner.TryBuyPositiveLevels(_lvlsAmount);
        }

        private void SellPositiveLevels()
        {
            _ = _earner.TrySellPositiveLevels(_lvlsAmount);
        }


        private void BuyNegativeLevels()
        {
            _ = _earner.TryBuyNegativeLevels(_lvlsAmount);
        }

        private void SellNegativeLevels()
        {
            _ = _earner.TrySellNegativeLevels(_lvlsAmount);
        }
    }
}
