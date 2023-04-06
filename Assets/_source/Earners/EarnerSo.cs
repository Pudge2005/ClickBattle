using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Game.Earners
{

    [CreateAssetMenu(menuName = GameConstants.Earners + "Earner")]
    public sealed class EarnerSo : SoDatabaseElement
    {
        [SerializeField] MetaInfo _metaInfo;

#if UNITY_EDITOR
        [SerializeField] private EarningModifiersCreator _positiveModsCreator;
        [SerializeField] private bool _createForPositives;
        [SerializeField] private EarningModifiersCreator _negativeModsCreator;
        [SerializeField] private bool _createForNegatives;
#endif
        [SerializeField] private EarningModifier[] _positiveModifiers;
        [SerializeField] private EarningModifier[] _negativeModifiers;

        public MetaInfo MetaInfo => _metaInfo;

        public IReadOnlyList<EarningModifier> PositiveModifiers => _positiveModifiers;
        public IReadOnlyList<EarningModifier> NegativeModifiers => _negativeModifiers;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_createForPositives)
            {
                _createForPositives = false;

                _positiveModifiers = _positiveModsCreator.CreateMods();
                UnityEditor.EditorUtility.SetDirty(this);
            }
            else if (_createForNegatives)
            {
                _createForNegatives = false;

                _negativeModifiers = _negativeModsCreator.CreateMods();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
