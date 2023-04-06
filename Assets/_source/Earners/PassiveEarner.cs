using UnityEngine;

namespace Game.Earners
{
    public sealed class PassiveEarner : EarnerOnScene_OLD
    {
        [SerializeField, Min(0.01f)] private float _tickrate = 10f;

        private float _cdLeft;


        private void Update()
        {
            if ((_cdLeft -= Time.deltaTime) > 0)
                return;

            _cdLeft = 1f / _tickrate;

            var earnPerSec = GetReward();
            var tickEarn = earnPerSec / _tickrate;
            ConfirmEarning(tickEarn);
        }
    }
}
