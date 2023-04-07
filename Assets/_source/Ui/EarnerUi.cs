using Game.Core;
using Game.Earners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public sealed class EarnerUi : MonoBehaviour
    {
        private const int _lvlsAmount = 1;

        [SerializeField] private Button _buyPositiveLevelsButton;
        [SerializeField] private Button _sellPositiveLevelsButton;

        [SerializeField] private Button _buyNegativeLevelsButton;
        [SerializeField] private Button _sellNegativeLevelsButton;

        [SerializeField] private TMP_Text _goodLvlText;
        [SerializeField] private TMP_Text _badLvlText;
        [SerializeField] private TMP_Text _badOpponentLvlText;

        [SerializeField] private TMP_Text _lvlsAmountText;
        [SerializeField] private TMP_Text _goodLvlsCostText;
        [SerializeField] private TMP_Text _badOpponentLvlsCostText;
                
        private IEarner _earner;


        public void Init(IEarner earner)
        {
            _earner = earner;
            _earner.PositiveLvlChanged += HandlePositiveLvlChanged;
            _earner.NegativeLvlChanged += HandleNegativeLvlChanged;
            _earner.OpponentNegativeLvlChanged += HandleOpponentNegativeLvlChanged;

            GameManager.BalanceChanged += HandleBalanceChanged;
            UpdateButtonsInteractivityState(GameManager.Balance);

            _buyPositiveLevelsButton.onClick.AddListener(BuyPositiveLevels);
            _sellPositiveLevelsButton.onClick.AddListener(SellPositiveLevels);
            _buyNegativeLevelsButton.onClick.AddListener(BuyNegativeLevels);
            _sellNegativeLevelsButton.onClick.AddListener(SellNegativeLevels);
        }


        private void HandlePositiveLvlChanged(IEarner earner, int prevLvl, int newLvl)
        {
            _goodLvlText.text = NumericsShortStringConverter.IntToShortString(newLvl);
            _goodLvlsCostText.text = NumericsShortStringConverter.FloatToShortString(earner.GetPositiveLevelsBuyCost(_lvlsAmount));
        }

        private void HandleNegativeLvlChanged(IEarner earner, int prevLvl, int newLvl)
        {
            _badLvlText.text = NumericsShortStringConverter.IntToShortString(newLvl);
        }

        private void HandleOpponentNegativeLvlChanged(IEarner earner, int prevLvl, int newLvl)
        {
            _badOpponentLvlText.text = NumericsShortStringConverter.IntToShortString(newLvl);
            _badOpponentLvlsCostText.text = NumericsShortStringConverter.FloatToShortString(earner.GetPositiveLevelsBuyCost(_lvlsAmount));
        }


        private void HandleBalanceChanged(GameManager gameManager, float newBalance, float delta)
        {
            UpdateButtonsInteractivityState(newBalance);
        }

        private void UpdateButtonsInteractivityState(float newBalance)
        {
            //check can buy/sell

            _buyPositiveLevelsButton.interactable = _earner.CanBuyPositiveLevels(_lvlsAmount);
            _sellPositiveLevelsButton.interactable = _earner.CanSellPositiveLevels(_lvlsAmount);

            _buyNegativeLevelsButton.interactable = _earner.CanBuyNegativeLevelsForOpponent(_lvlsAmount);
            _sellPositiveLevelsButton.interactable = _earner.CanSellNegativeLevelsForOpponent(_lvlsAmount);
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
            _ = _earner.TryBuyNegativeLevelsForOpponent(_lvlsAmount);
        }

        private void SellNegativeLevels()
        {
            _ = _earner.TrySellNegativeLevelsForOpponent(_lvlsAmount);
        }
    }
}
