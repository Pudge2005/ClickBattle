using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Upgrades
{


    [CreateAssetMenu(menuName = "Game/Upgrades/Passive Earner")]
    public sealed class PassiveEarnerSo : SoDatabaseElement
    {
        [System.Serializable]
        public sealed class Level
        {
            [SerializeField] private int _earnsPerMinute;
            [SerializeField] private float _earning;


            public int EarnsPerMinute => _earnsPerMinute;
            public float Earning => _earning;
        }


        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private Level[] _levels;


        public MetaInfo MetaInfo => _metaInfo;
        public IReadOnlyList<Level> Levels => _levels;
    }
}
