using Game.Core;
using Game.Modifiers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Clicker
{
    public interface IEarner
    {
        int PositiveLevel { get; set; }
        int NegativeLevel { get; set; }


    }
    public sealed class UpgradableEarnerUi : MonoBehaviour
    {
        [SerializeField] private EarnerOnScene _earner;

        [SerializeField] private Button _positiveLvlUpButton;
        [SerializeField] private Button _positiveLvlDownButton;

        [SerializeField] private Button _negativeLvlUpButton;
        [SerializeField] private Button _negativeLvlDownButton;


        private void Start()
        {
            _positiveLvlUpButton.onClick.AddListener(LvlUpPositive);
        }

        private void LvlUpPositive()
        {
            _earner.
        }
    }

    public abstract class EarnerOnScene : MonoBehaviour
    {
        public delegate void EarnedDelegate(EarnerOnScene earnerOnScene, float reward);


        [SerializeField] private float _defaultReward = 10f;
        [SerializeField] private RewardModifierSo _rewardModifier;
        [SerializeField] private EarnModifiersCollection _modifiersCollection;


        public RewardModifierSo RewardModifier => _rewardModifier;


        public event EarnedDelegate Earned;




        protected float GetReward()
        {
            return _modifiersCollection.ProcessReward(_rewardModifier, _defaultReward);
        }


        protected void ConfirmEarning(float confirmedEarning)
        {
            GameManager.RegisterEarning(confirmedEarning);
            Earned?.Invoke(this, confirmedEarning);
        }
    }
}
