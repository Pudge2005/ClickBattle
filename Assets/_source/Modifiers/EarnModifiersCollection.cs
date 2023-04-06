using System;
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


        public void FillOptimizer(ModifierOptimizer optimizer)
        {
            //negative income is not allowed
            if (_positiveModifierLvl < 0)
                return;

            var mod = _modifier.PositiveMods[_positiveModifierLvl];
            float flat = mod.FlatDelta;
            float mult = mod.MultiplierDelta;

            if (_negativeModifierLvl < 0)
                return;

            mod = _modifier.NegativeMods[_negativeModifierLvl];
            flat += mod.FlatDelta;
            mult += mod.MultiplierDelta;

            if (flat > 0)
                optimizer.AddFlatDelta(flat);

            if (mult > 0)
                optimizer.AddMultiplierDelta(mult);
        }
    }

    //и для кликов и для пассивных earners будет один тип RewardModifierSo
    //для кликов он обрабатывает каждый клик, а для пассивных - каждый тик
    //обрабатывается через оптимайзер, оптимайзер обновляется при добавлении
    //или удалении или изменении уровня модифаерСО

    public sealed class EarnModifiersCollection : MonoBehaviour
    {
        private sealed class Helper
        {
            private readonly Dictionary<RewardModifierSo, RewardModifierData> _modifiers = new();
            private readonly ModifierOptimizer _optimizer = new();


            public void SetPositiveLevel(RewardModifierSo modifier, int level)
            {
                SetLevel(modifier, level, true);
            }

            public void SetNegativeLevel(RewardModifierSo modifier, int level)
            {
                SetLevel(modifier, level, false);
            }


            public void SetLevel(RewardModifierSo modifier, int level, bool isPositive)
            {
                var data = GetOrCreate(modifier);

                if (isPositive)
                    data.PositiveModifierLvl = level;
                else
                    data.NegativeModifierLvl = level;

                UpdateOptimizer();
            }


            public float ProcessReward(float source)
            {
#if UNITY_EDITOR
                var v = _optimizer.ProcessValue(source);

                if (v < 0)
                    throw new Exception($"negative reward is not allowed: {source}, {v}");

                return v;
#else
                return _optimizer.ProcessValue(source);
#endif
            }


            private RewardModifierData GetOrCreate(RewardModifierSo rewardModifierSo)
            {
                if (_modifiers.TryGetValue(rewardModifierSo, out var data))
                {
                    return data;
                }

                return AddMod(rewardModifierSo);
            }

            private RewardModifierData AddMod(RewardModifierSo rewardModifierSo)
            {
                var data = new RewardModifierData();
                data.Init(rewardModifierSo);
                _modifiers.Add(rewardModifierSo, data);
                return data;
            }

            private void UpdateOptimizer()
            {
                _optimizer.Reset();

                foreach (var mod in _modifiers)
                {
                    mod.Value.FillOptimizer(_optimizer);
                }
            }
        }


        [SerializeField] private NetworkGameManager _networkGameManager;

        private readonly Helper _active = new();
        private readonly Helper _passive = new();


        
        //public float Process
    }
}
