using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Game.Modifiers
{
#if UNITY_EDITOR
    [System.Serializable]
    internal sealed class ModsCreator
    {
        [SerializeField] private int _levelsCount;
        [SerializeField] private AnimationCurve _flatCurve;
        [SerializeField] private AnimationCurve _multCurve;


        public Modifier[] CreateMods()
        {
            Modifier[] mods = new Modifier[_levelsCount];

            for (int i = 0; i < _levelsCount; i++)
            {
                float t = (float)i / (float)(_levelsCount - 1);

                if (float.IsSubnormal(t))
                    t = 0f;

                mods[i] = new Modifier(_flatCurve.Evaluate(t), _multCurve.Evaluate(t));
            }

            return mods;
        }
    }
#endif

    [System.Serializable]
    public sealed class Modifier
    {
        [Tooltip("Absolute value")]
        [SerializeField, Min(0f)] private float _flatDelta;
        [Tooltip("Absolute value")]
        [SerializeField, Min(0f)] private float _multiplierDelta;


        public Modifier(float flatDelta, float multiplier)
        {
            _flatDelta = flatDelta;
            _multiplierDelta = multiplier;
        }


        public float FlatDelta => _flatDelta;
        public float MultiplierDelta => _multiplierDelta;
    }


    [CreateAssetMenu(menuName = GameConstants.Modifiers + "Reward Modifier")]
    public sealed class RewardModifierSo : SoDatabaseElement
    {
        [SerializeField] private MetaInfo _metaInfo;

#if UNITY_EDITOR
        [SerializeField] private ModsCreator _positiveModsCreator;
        [SerializeField] private bool _createForPositives;
        [SerializeField] private ModsCreator _negativeModsCreator;
        [SerializeField] private bool _createForNegatives;
#endif
        [SerializeField] private Modifier[] _positiveMods;
        [SerializeField] private Modifier[] _negativeMods;


        public MetaInfo MetaInfo => _metaInfo;
        public IReadOnlyList<Modifier> PositiveMods => _positiveMods;
        public IReadOnlyList<Modifier> NegativeMods => _negativeMods;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_createForPositives)
            {
                _createForPositives = false;

                _positiveMods = _positiveModsCreator.CreateMods();
                UnityEditor.EditorUtility.SetDirty(this);
            }
            else if (_createForNegatives)
            {
                _createForNegatives = false;

                _negativeMods = _negativeModsCreator.CreateMods();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
