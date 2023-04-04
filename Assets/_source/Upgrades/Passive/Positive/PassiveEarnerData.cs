namespace Game.Upgrades
{
    public sealed class PassiveEarnerData
    {
        private readonly PassiveEarnerSo _earner;
        private int _level;


        public PassiveEarnerData(PassiveEarnerSo earner, int level = 0)
        {
            _earner = earner;
            _level = 0;
        }


        public PassiveEarnerSo Earner => _earner;
        public int Level => _level;


        public void SetLevel(int level)
        {
            _level = level;
        }
    }
}
