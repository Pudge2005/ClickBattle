using Game.Core;
using UnityEngine;

namespace Game.Earners
{
    public abstract class Earner : MonoBehaviour, IEarner
    {
        [SerializeField] private EarnerSo _reference;

        private int _maxPositiveLvl;
        private int _maxNegLvl;

        private int _posLvl = -1;
        private int _negLvl = -1;
        private int _opNegLvl = -1;


        public EarnerSo EarnerSo => _reference;

        public int MaxPositiveLvl => _maxPositiveLvl;
        public int MaxNegativeLvl => _maxNegLvl;

        public int PositiveLvl => _posLvl;
        public int NegativeLvl => _negLvl;
        public int OpponentNegativeLvl => _opNegLvl;


        public event IEarner.LvlChangedDelegate PositiveLvlChanged;
        public event IEarner.LvlChangedDelegate NegativeLvlChanged;
        public event IEarner.LvlChangedDelegate OpponentNegativeLvlChanged;


        protected virtual void Start()
        {
            Init(_reference);
        }

        public void Init(EarnerSo so)
        {
            _reference = so;
            _maxPositiveLvl = so.PositiveModifiers.Count - 1;
            _maxNegLvl = so.NegativeModifiers.Count - 1;
        }


        public void SetPositiveLvl(int lvl)
        {
            if (lvl == _posLvl)
                return;

            var prev = _posLvl;
            _posLvl = lvl;

            PositiveLvlChanged?.Invoke(this, prev, lvl);
        }

        public void SetNegativeLvl(int lvl)
        {
            if (lvl == _negLvl)
                return;

            var prev = _negLvl;
            _negLvl = lvl;

            NegativeLvlChanged?.Invoke(this, prev, lvl);
        }

        public void SetOpponentNegativeLvl(int lvl)
        {
            if (lvl == _opNegLvl)
                return;

            var prev = _opNegLvl;
            _opNegLvl = lvl;

            OpponentNegativeLvlChanged?.Invoke(this, prev, lvl);
        }


        internal bool CanBuyPositiveLevels(int lvls, out float totalCost)
        {
            int desiredLvl = _posLvl + lvls;
            totalCost = 0;

            if (desiredLvl > MaxPositiveLvl)
                return false;

            float balance = GameManager.Balance;
            var mods = _reference.PositiveModifiers;

            for (int i = 0, lvl = _posLvl + 1; i < lvls; ++i, ++lvl)
            {
                totalCost += mods[lvl].BuyPrice;

                if (balance < totalCost)
                    return false;
            }

            return true;
        }

        public bool CanBuyPositiveLevels(int lvls)
        {
            return CanBuyPositiveLevels(lvls, out _);
        }

        public bool TryBuyPositiveLevels(int lvls)
        {
            if (!CanBuyPositiveLevels(lvls, out var totalCost))
                return false;

            GameManager.Spend(totalCost);
            SetPositiveLvl(_posLvl + lvls);

            return true;
        }


        internal bool CanSellPositiveLevels(int lvlsAmount, out float refund)
        {
            refund = 0;
            int desiredLvl = _posLvl - lvlsAmount;

            if (desiredLvl < -1)
                return false;

            var mods = _reference.PositiveModifiers;

            for (int i = 0, lvl = _posLvl + 1; i < lvlsAmount; ++i, ++lvl)
            {
                refund += mods[lvl].SellPrice;
            }

            return true;
        }

        public bool CanSellPositiveLevels(int lvls)
        {
            return CanSellPositiveLevels(lvls, out _);
        }

        public bool TrySellPositiveLevels(int lvls)
        {
            if (!CanSellPositiveLevels(lvls, out var refund))
                return false;

            GameManager.Earn(refund);
            SetPositiveLvl(_posLvl - lvls);
            return true;
        }


        internal bool CanBuyNegativeLevelsForOpponent(int lvls, out float totalCost)
        {
            totalCost = 0;
            int desiredLvl = _opNegLvl + lvls;

            if (desiredLvl > _maxNegLvl)
                return false;

            float balance = GameManager.Balance;
            var mods = _reference.NegativeModifiers;

            for (int i = 0, lvl = _opNegLvl + 1; i < lvls; ++i, ++lvl)
            {
                totalCost += mods[lvl].BuyPrice;

                if (balance < totalCost)
                    return false;
            }

            return true;
        }

        public bool CanBuyNegativeLevelsForOpponent(int lvls)
        {
            return CanBuyNegativeLevelsForOpponent(lvls, out _);
        }

        public bool TryBuyNegativeLevels(int lvls)
        {
            if (!CanBuyNegativeLevelsForOpponent(lvls, out var totalCost))
                return false;

            GameManager.Spend(totalCost);
            SetOpponentNegativeLvl(_opNegLvl + lvls);

            return true;
        }


        internal bool CanSellNegativeLevelsForOpponent(int lvls, out float refund)
        {
            refund = 0;

            int desiredLvl = _opNegLvl - lvls;

            if (desiredLvl < -1)
                return false;

            float totalRefund = 0;
            var mods = _reference.NegativeModifiers;

            for (int i = 0, lvl = _opNegLvl + 1; i < lvls; ++i, ++lvl)
            {
                totalRefund += mods[lvl].SellPrice;
            }

            return true;
        }

        public bool CanSellNegativeLevelsForOpponent(int lvls)
        {
            return CanSellNegativeLevelsForOpponent(lvls, out _);
        }


        public bool TrySellNegativeLevels(int lvls)
        {
            if (!CanSellNegativeLevelsForOpponent(lvls, out var refund))
                return false;

            GameManager.Earn(refund);
            SetOpponentNegativeLvl(_opNegLvl - lvls);

            return true;
        }


        public float GetEarning()
        {
            if (_posLvl < 0)
                return 0f;

            var positive = _reference.PositiveModifiers[_posLvl];
            float flat = positive.Flat;
            float mult = positive.Mult;

            if (_negLvl >= 0)
            {
                var negative = _reference.NegativeModifiers[_negLvl];
                flat += negative.Flat;
                mult += negative.Mult;
            }

            float earning = flat * (1f + mult);

            if (earning < 0)
                earning = 0;

            return earning;
        }


        public void ConfirmEarning(float earning)
        {
            GameManager.Earn(earning);
        }
    }
}
