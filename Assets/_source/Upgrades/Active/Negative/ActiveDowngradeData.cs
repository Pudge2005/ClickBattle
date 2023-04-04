namespace Game.Upgrades
{
    public sealed class ActiveDowngradeData
    {
        private readonly ActiveDowngradeSo _downgrade;
        private int _level;


        /// <param name="level">INDEX, DO NOT START FROM 1</param>
        public ActiveDowngradeData(ActiveDowngradeSo affector, int level = 0)
        {
            _downgrade = affector;
            _level = level;
        }


        public ActiveDowngradeSo Affector => _downgrade;
        public int Level => _level;


        public void SetLevel(int newLevel)
        {
            _level = newLevel;
        }

        public float Process(float value)
        {
            return _downgrade.Process(value, _level);
        }


        public override int GetHashCode()
        {
            return _downgrade.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ActiveDowngradeData other
                && other.GetHashCode() == GetHashCode();
        }
    }
}
