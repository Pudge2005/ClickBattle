using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Game.Upgrades
{
    // can be used as permenent downgrade or temp debuff
    [CreateAssetMenu(menuName = "Game/Upgrades/Passive Earner Debuff")]
    public sealed class PassiveEarnerDowngradeSo : SoDatabaseElement
    {
        [System.Serializable]
        public sealed class Level
        {
            [SerializeField, Min(0f)] private float _flatDecreasing = 0;
            [SerializeField, Range(0f, 100f)] private float _persentageDecreasing = 10f;


            public float Process(float source)
            {
                float finalDecreasing = source * _persentageDecreasing - _flatDecreasing;
                return source - finalDecreasing;
            }
        }


        [SerializeField] private MetaInfo _metaInfo;
        [Tooltip("Opponent can lose points (Level.Process(...) can return negative value)"]
        [SerializeField] private bool _allowNegativeEarning = false;
        [SerializeField] private PassiveEarnerSo _earner;
        [SerializeField] private Level[] _levels;


        public MetaInfo MetaInfo => _metaInfo;
        public IReadOnlyList<Level> Levels => _levels;


        public float Process(float source, int level)
        {
            var processed = _levels[level].Process(source);

            if (!_allowNegativeEarning && processed < 0)
                processed = 0;

            return processed;
        }
    }
}
