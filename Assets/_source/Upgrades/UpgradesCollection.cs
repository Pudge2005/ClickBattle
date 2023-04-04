using System.Collections.Generic;
using UnityEngine;

namespace Game.Upgrades
{
    public sealed class UpgradesCollection : MonoBehaviour
    {
        private sealed class AcitiveUpgradeInfo
        {
            private readonly ActiveUpgradeSo _upgrade;
            private int _upgradeLevel;

            private readonly List<ActiveDowngradeData> _downgrades = new();
        }


        
    }

    public sealed class DebuffData
    {
        private readonly PassiveEarnerSo _earner;
        private readonly float _timeToLive;
        private float _timeToLiveLeft;


        public DebuffData(PassiveEarnerSo earner, float timeToLive)
        {
            _earner = earner;
            _timeToLive = _timeToLiveLeft = timeToLive;
        }


        public PassiveEarnerSo Earner => _earner;
        public float TimeToLive => _timeToLive;
        public float TimeToLiveLeft => _timeToLiveLeft;
    }
}
