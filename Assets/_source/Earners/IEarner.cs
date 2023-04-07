using System;
using System.Runtime.Remoting.Metadata;

namespace Game.Earners
{
    public interface IEarner
    {
        delegate void LvlChangedDelegate(IEarner data, int prevLvl, int newLvl);


        EarnerSo EarnerSo { get; }

        int MaxPositiveLvl { get; }
        int MaxNegativeLvl { get; }

        int PositiveLvl { get; }
        int NegativeLvl { get; }
        int OpponentNegativeLvl { get; }


        event LvlChangedDelegate PositiveLvlChanged;
        event LvlChangedDelegate NegativeLvlChanged;
        event LvlChangedDelegate OpponentNegativeLvlChanged;


        bool TryBuyPositiveLevels(int lvlsAmount);

        bool TrySellPositiveLevels(int lvlsAmount);

        bool TryBuyNegativeLevels(int lvlsAmount);

        bool TrySellNegativeLevels(int lvlsAmount);


        float GetEarning();

        void ConfirmEarning(float earning);
    }
}
