using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Game.Upgrades
{
    [CreateAssetMenu(menuName = "Game/Active Upgrade")]
    public sealed class ActiveUpgradeSo : SoDatabaseElement
    {
        [System.Serializable]
        public sealed class Level
        {
            [SerializeField, Min(0f)] private float _flatBonus;
            [SerializeField, Min(1f)] private float _multiplier = 1f;


            public float FlatBonus => _flatBonus;
            public float Multiplier => _multiplier;


            public float Process(float source)
            {
                return source * _multiplier + _flatBonus;
            }
        }


        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private Level[] _levels;


        public MetaInfo MetaInfo => _metaInfo;
        public IReadOnlyList<Level> Levels => _levels;


        public float Process(float source, int upgradeLvl)
        {
            return _levels[upgradeLvl].Process(source);
        }
    }
}
