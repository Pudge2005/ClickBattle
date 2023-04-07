using Game.AntiCheats;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Earners.Clicker
{
    public sealed class ClickTarget : Earner, IPointerDownHandler
    {
        public delegate void ClickRegisteredDelegate(ClickTarget click, float bounty, Vector3 clickWorldPosition);


        [SerializeField] private AntiAutoClicker _antiAutoClicker;
        [SerializeField] private bool _upg;


        public event ClickRegisteredDelegate ClickRegistered;


        protected override void Start()
        {
            base.Start();
            ConfirmEarning(9000);
        }

        private void Update()
        {
            if (_upg)
            {
                _upg = false;
                var lvldUp = TryBuyPositiveLevels(1);
                Debug.Log(lvldUp);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("attempt");
            if (!_antiAutoClicker.ShouldRegisterClick())
                return;

            Debug.Log("click registered");
            var earning = GetEarning();
            Debug.Log(earning);
            ClickRegistered?.Invoke(this, earning, eventData.pointerPressRaycast.worldPosition);
            ConfirmEarning(earning);
        }
    }
}
