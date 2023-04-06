namespace Game.Modifiers
{
    public sealed class ModifierOptimizer
    {
        private float _flat;
        private float _mult;


        public void Reset()
        {
            _flat = 0;
            _mult = 0;
        }

        public void AddFlatDelta(float flatDelta)
        {
            _flat += flatDelta;
        }

        public void AddMultiplierDelta(float multiplierDelta)
        {
            _mult += multiplierDelta;
        }


        public float ProcessValue(float source)
        {
            return source * ((1 + _mult) + _flat);
        }
    }
}
