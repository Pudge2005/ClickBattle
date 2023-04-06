using Game.Core;
using UnityEngine;

namespace Game.Earners
{
    public abstract class Earner : MonoBehaviour, IEarner
    {
        [SerializeField] private EarnerSo _reference;

        private EarnerData _data;


        public IReadOnlyEarnerData Data => _data;


        public void InitEarner(EarnerSo earnerSo)
        {
            //lined up for testing
            //_reference = earnerSo;
        }


        internal bool CanBuyPositiveLevels(int lvls, out float totalCost)
        {
            int desiredLvl = _data.PositiveLvl + lvls;
            totalCost = 0;

            if (desiredLvl > _data.MaxPositiveLvl)
                return false;

            float balance = GameManager.Balance;
            var mods = _reference.PositiveModifiers;

            for (int i = 0, lvl = _data.PositiveLvl + 1; i < lvls; ++i, ++lvl)
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
            SetPositiveLvl(_data.PositiveLvl + lvls);

            return true;
        }


        internal bool CanSellPositiveLevels(int lvlsAmount, out float refund)
        {
            refund = 0;
            int desiredLvl = _data.PositiveLvl - lvlsAmount;

            if (desiredLvl < -1)
                return false;

            var mods = _reference.PositiveModifiers;

            for (int i = 0, lvl = _data.PositiveLvl + 1; i < lvlsAmount; ++i, ++lvl)
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
            SetPositiveLvl(_data.PositiveLvl - lvls);
            return true;
        }


        internal bool CanBuyNegativeLevelsForOpponent(int lvls, out float totalCost)
        {
            totalCost = 0;
            int desiredLvl = _data.OpponentNegativeLvl + lvls;

            if (desiredLvl > _data.MaxNegativeLvl)
                return false;

            float balance = GameManager.Balance;
            var mods = _reference.NegativeModifiers;

            for (int i = 0, lvl = _data.OpponentNegativeLvl + 1; i < lvls; ++i, ++lvl)
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
            SetOpponentNegativeLvl(_data.OpponentNegativeLvl + lvls);

            return true;
        }


        internal bool CanSellNegativeLevelsForOpponent(int lvls, out float refund)
        {
            refund = 0;

            int desiredLvl = _data.OpponentNegativeLvl - lvls;

            if (desiredLvl < -1)
                return false;

            float totalRefund = 0;
            var mods = _reference.NegativeModifiers;

            for (int i = 0, lvl = _data.OpponentNegativeLvl + 1; i < lvls; ++i, ++lvl)
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
            SetOpponentNegativeLvl(_data.OpponentNegativeLvl - lvls);

            return true;
        }


        private void SetPositiveLvl(int lvl)
        {
            _data.SetPositiveLvl(lvl);
        }

        private void SetNegativeLvl(int lvl)
        {
            _data.SetNegativeLvl(lvl);
        }

        private void SetOpponentNegativeLvl(int lvl)
        {
            _data.SetOpponentNegativeLvl(lvl);
        }

    }
}
