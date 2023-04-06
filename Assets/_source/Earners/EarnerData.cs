namespace Game.Earners
{
    public sealed class EarnerData : IReadOnlyEarnerData
    {
        private readonly EarnerSo _so;
        private readonly int _maxPositiveLvl;
        private readonly int _maxNegativeLvl;

        private int _posLvl = -1;
        private int _negLvl = -1;
        private int _opNegLvl = -1;


        public EarnerData(EarnerSo so)
        {
            _so = so;
            _maxPositiveLvl = so.PositiveModifiers.Count - 1;
            _maxNegativeLvl = so.NegativeModifiers.Count - 1;
        }


        public EarnerSo EarnerSo => _so;

        public int MaxPositiveLvl => _maxPositiveLvl;
        public int MaxNegativeLvl => _maxNegativeLvl;

        public int PositiveLvl => _posLvl;
        public int NegativeLvl => _negLvl;
        public int OpponentNegativeLvl => _opNegLvl;


        public event IReadOnlyEarnerData.LvlChangedDelegate PositiveLvlChanged;
        public event IReadOnlyEarnerData.LvlChangedDelegate NegativeLvlChanged;
        public event IReadOnlyEarnerData.LvlChangedDelegate OpponentNegativeLvlChanged;


        public void SetPositiveLvl(int lvl)
        {
            if (lvl == _posLvl)
                return;

            var prev = _posLvl;
            _posLvl = lvl;

            PositiveLvlChanged?.Invoke(this, prev, lvl);
        }

        public void SetNegativeLvl(int lvl)
        {
            if (lvl == _negLvl)
                return;

            var prev = _negLvl;
            _negLvl = lvl;

            NegativeLvlChanged?.Invoke(this, prev, lvl);
        }

        public void SetOpponentNegativeLvl(int lvl)
        {
            if (lvl == _opNegLvl)
                return;

            var prev = _opNegLvl;
            _opNegLvl = lvl;

            OpponentNegativeLvlChanged?.Invoke(this, prev, lvl);
        }
    }
}
