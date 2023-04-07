namespace Game.Earners
{
    public interface IEarner
    {
        delegate void LvlChangedDelegate(IEarner earner, int prevLvl, int newLvl);


        EarnerSo EarnerSo { get; }

        int MaxPositiveLvl { get; }
        int MaxNegativeLvl { get; }

        int PositiveLvl { get; }
        int NegativeLvl { get; }
        int OpponentNegativeLvl { get; }


        event LvlChangedDelegate PositiveLvlChanged;
        event LvlChangedDelegate NegativeLvlChanged;
        event LvlChangedDelegate OpponentNegativeLvlChanged;


        float GetPositiveLevelsBuyCost(int lvls);
        float GetNegativeLevelsForOpponentBuyCost(int lvls);

        float GetPositiveLevelsSellCost(int lvls);
        float GetNegativeLevelsForOpponentSellCost(int lvls);

        bool CanBuyPositiveLevels(int lvls);
        bool TryBuyPositiveLevels(int lvls);

        bool CanSellPositiveLevels(int lvls);
        bool TrySellPositiveLevels(int lvls);

        bool CanBuyNegativeLevelsForOpponent(int lvls);
        bool TryBuyNegativeLevelsForOpponent(int lvls);

        bool CanSellNegativeLevelsForOpponent(int lvls);
        bool TrySellNegativeLevelsForOpponent(int lvls);


        float GetEarning();

        void ConfirmEarning(float earning);
    }
}
