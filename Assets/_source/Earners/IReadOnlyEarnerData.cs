namespace Game.Earners
{
    public interface IReadOnlyEarnerData
    {
        delegate void LvlChangedDelegate(IReadOnlyEarnerData data, int prevLvl, int newLvl);


        EarnerSo EarnerSo { get; }

        int MaxPositiveLvl { get; }
        int MaxNegativeLvl { get; }

        int PositiveLvl { get; }
        int NegativeLvl { get; }
        int OpponentNegativeLvl { get; }


        event LvlChangedDelegate PositiveLvlChanged;
        event LvlChangedDelegate NegativeLvlChanged;
        event LvlChangedDelegate OpponentNegativeLvlChanged;
    }
}
