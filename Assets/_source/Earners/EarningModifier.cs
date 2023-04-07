using UnityEngine;

namespace Game.Earners
{
#if UNITY_EDITOR
    [System.Serializable]
    internal sealed class EarningModifiersCreator
    {
        [SerializeField] private int _levelsCount;
        [SerializeField] private AnimationCurve _flatCurve;
        [SerializeField] private AnimationCurve _multCurve;
        [SerializeField] private AnimationCurve _buyPriceCurve;
        [SerializeField] private AnimationCurve _sellPriceCurve;


        public EarningModifier[] CreateMods()
        {
            EarningModifier[] mods = new EarningModifier[_levelsCount];

            for (int i = 0; i < _levelsCount; i++)
            {
                float t = (float)i / (float)(_levelsCount - 1);

                if (float.IsSubnormal(t))
                    t = 0f;

                mods[i] = new EarningModifier(_flatCurve.Evaluate(t),
                    _multCurve.Evaluate(t), _buyPriceCurve.Evaluate(t),
                    _sellPriceCurve.Evaluate(t));
            }

            return mods;
        }
    }
#endif

    [System.Serializable]
    public sealed class EarningModifier
    {
        private const bool _allowNegativeEarning = false;

        [SerializeField] private float _flat;
        [SerializeField] private float _mult;
        [SerializeField] private float _buyPrice;
        [SerializeField] private float _sellPrice;


        public EarningModifier(float flat, float mult, float buyPrice, float sellPrice)
        {
            _flat = flat;
            _mult = mult;
            _buyPrice = buyPrice;
            _sellPrice = sellPrice;
        }


        public float Flat => _flat;
        public float Mult => _mult;
        public float BuyPrice => _buyPrice;
        public float SellPrice => _sellPrice;


        public float Modify(float source)
        {
            float modified = (source + _flat) * (1f + _mult);

            if (modified < 0 && !_allowNegativeEarning)
                return 0;

            return modified;
        }
    }
}
