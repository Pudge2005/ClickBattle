using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using Game.Earners;

namespace Game.Core
{
    public interface INetworkGameManager
    {
        delegate void BalanceChangedDelegate(float newBalance);
        delegate void EarnerNegativeLevelChangedDelegate(EarnerSo earner, int lvl);
        delegate void GameEndedDelegate(bool won);


        float WinBalance { get; }
        float OpponentBalance { get; }

        IReadOnlyDictionary<EarnerSo, int> EarnerNegativeLevels { get; }


        event BalanceChangedDelegate OpponentBalanceChanged;
        event EarnerNegativeLevelChangedDelegate EarnerNegativeLevelChanged;
        event GameEndedDelegate GameEnded;


        void SetPlayerBalance(float playerBalance);

        void SetOpponentEarnerNegativeLevel(EarnerSo earner, int lvl);
    }
}
