using System;
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
                GameManager.NetworkGameManagerInited += GameManager_NetworkGameManagerInited;
            }
            else
            {
                GameManager_NetworkGameManagerInited(GameManager.NetworkGameManager);
            }
        }

        private void HandlePlayerBalanceChanged(GameManager gameManager, float newBalance, float delta)
        {
            _playerBalanceText.text = newBalance.ToString("N0");

        }

        private void GameManager_NetworkGameManagerInited(INetworkGameManager_OLD ngm)
        {

        }
    }
}
