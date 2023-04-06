using System;
using System.Runtime.Remoting.Metadata;

namespace Game.Earners
{
    public interface IEarner
    {
        IReadOnlyEarnerData Data { get; }


        bool TryBuyPositiveLevels(int lvlsAmount);

        bool TrySellPositiveLevels(int lvlsAmount);

        bool TryBuyNegativeLevels(int lvlsAmount);

        bool TrySellNegativeLevels(int lvlsAmount);

    }
}
