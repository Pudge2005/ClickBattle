using Game.Processors;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Clicker
{
    public sealed class PassiveEarner : EarnerOnScene
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
    public sealed class ClickTarget : EarnerOnScene, IPointerDownHandler
    {
        public delegate void ClickRegisteredDelegate(ClickTarget click, float bounty, Vector3 clickWorldPosition);


        [SerializeField] private AntiAutoClicker _antiAutoClicker;


        public event ClickRegisteredDelegate ClickRegistered;


        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_antiAutoClicker.ShouldRegisterClick())
                return;

            var reward = GetReward();

            ClickRegistered?.Invoke(this, reward, eventData.pointerPressRaycast.worldPosition);
            ConfirmEarning(reward);
        }
    }
}
