using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public sealed class EndGameUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;

        [SerializeField] private Button _exitButton;
    }
}
