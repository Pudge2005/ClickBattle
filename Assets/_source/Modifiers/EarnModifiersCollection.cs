using System.Collections.Generic;
using UnityEngine;

namespace Game.Modifiers
{
    public sealed class RewardModifierData
    {
        private RewardModifierSo _modifier;
        private int _positiveModifierLvl;
        private int _negativeModifierLvl;


        //poolable in perspective
        public void Init(RewardModifierSo modifier)
        {
            _modifier = modifier;
            _positiveModifierLvl = -1;
            _negativeModifierLvl = -1;
        }


        public RewardModifierSo Modifier => _modifier;

        public int PositiveModifierLvl
        {
            get => _positiveModifierLvl;
            set => _positiveModifierLvl = value;
        }

        public int NegativeModifierLvl
        {
            get => _negativeModifierLvl;
            set => _negativeModifierLvl = value;
        }


        public float Process(float source)
        {
            //negative income is not allowed
            if (_positiveModifierLvl < 0)
                return 0;

            var mod = _modifier.PositiveMods[_positiveModifierLvl];
            float flat = mod.FlatDelta;
            float mult = mod.MultiplierDelta;

            if (_negativeModifierLvl >= 0)
            {
                mod = _modifier.NegativeMods[_negativeModifierLvl];
                flat += mod.FlatDelta;
                mult += mod.MultiplierDelta;
            }

            float result = (source + flat) * (1f + mult);

            if (result < 0)
                result = 0;

            return result;
        }
    }

    //и для кликов и для пассивных earners будет один тип RewardModifierSo
    //для кликов он обрабатывает каждый клик, а для пассивных - каждый тик
    //обрабатывается через оптимайзер, оптимайзер обновляется при добавлении
    //или удалении или изменении уровня модифаерСО

    public sealed class EarnModifiersCollection : MonoBehaviour
    {
        /// <summary>
        /// includes click (active) reward modifier
        /// </summary>
        private readonly Dictionary<RewardModifierSo, RewardModifierData> _earners = new();

        private INetworkGameManager _networkGameManager;


        public void Init(INetworkGameManager networkGameManager)
        {
            _networkGameManager = networkGameManager;
            _networkGameManager.ModifierLevelChanged += HandleModifierLevelChanged;
        }


        public float ProcessReward(RewardModifierSo modifier, float value)
        {
            if (!_earners.TryGetValue(modifier, out var data))
                return 0;

            return data.Process(value);
        }


        public void ChangeModifierLevel(RewardModifierSo modifier, int lvl, bool isPositive)
        {
            HandleModifierLevelChanged(modifier, lvl, isPositive);
        }

        private void HandleModifierLevelChanged(RewardModifierSo modifier, int lvl, bool isPositive)
        {
            var data = GetOrCreateData(modifier);

            if (isPositive)
                data.PositiveModifierLvl = lvl;
            else
                data.NegativeModifierLvl = lvl;
        }

        private RewardModifierData GetOrCreateData(RewardModifierSo rewardModifierSo)
        {
            if (!_earners.TryGetValue(rewardModifierSo, out var data))
            {
                data = new();
                data.Init(rewardModifierSo);
                _earners.Add(rewardModifierSo, data);
            }

            return data;
        }
    }
}
