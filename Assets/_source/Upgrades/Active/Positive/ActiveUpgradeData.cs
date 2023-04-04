namespace Game.Upgrades
{
    public sealed class ActiveUpgradeData
    {
        private readonly ActiveUpgradeSo _upgrade;
        private int _level;


        /// <param name="level">INDEX, DO NOT START FROM 1</param>
        public ActiveUpgradeData(ActiveUpgradeSo upgrade, int level = 0)
        {
            _upgrade = upgrade;
            _level = level;
        }


        public ActiveUpgradeSo Upgrade => _upgrade;
        public int Level => _level;


        public void SetLevel(int newLevel)
        {
            _level = newLevel;
        }

        public float Process(float value)
        {
            return _upgrade.Process(value, _level);
        }


        public override int GetHashCode()
        {
            return _upgrade.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ActiveUpgradeData other
                && other.GetHashCode() == GetHashCode()
                && other._level == _level;
        }
    }
}
