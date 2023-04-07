using Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public sealed class GameUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerBalanceText;
        [SerializeField] private Slider _playerWinProgressSlider;
        [SerializeField] private Slider _opponentWinProgressSlider;


        private void Start()
        {
            GameManager.BalanceChanged += HandlePlayerBalanceChanged;

            if (GameManager.NetworkGameManager == null)
            {
                GameManager.NetworkGameManagerInited += HandleNetworkGameManagerInited;
            }
            else
            {
                HandleNetworkGameManagerInited(GameManager.NetworkGameManager);
            }
        }

        private void HandlePlayerBalanceChanged(GameManager gameManager, float newBalance, float delta)
        {
            _playerBalanceText.text = newBalance.ToString("N0");
            _playerWinProgressSlider.value = newBalance;
        }

        private void HandleNetworkGameManagerInited(INetworkGameManager ngm)
        {
            _playerWinProgressSlider.minValue = 0;
            _opponentWinProgressSlider.minValue = 0;
            _playerWinProgressSlider.maxValue = ngm.WinBalance;
            _opponentWinProgressSlider.maxValue = ngm.WinBalance;

            ngm.OpponentBalanceChanged += HandleOpponentBalanceChanged;
            HandleOpponentBalanceChanged(ngm.OpponentBalance);
        }

        private void HandleOpponentBalanceChanged(float newBalance)
        {
            _opponentWinProgressSlider.value = newBalance;
        }
    }
}
