using Game.AntiCheats;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Earners.Clicker
{
    public sealed class ClickTarget : Earner, IPointerDownHandler
    {
        public delegate void ClickRegisteredDelegate(ClickTarget click, float bounty, Vector3 clickWorldPosition);


        [SerializeField] private AntiAutoClicker _antiAutoClicker;


        public event ClickRegisteredDelegate ClickRegistered;


        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_antiAutoClicker.ShouldRegisterClick())
                return;

            var earning = ();

            ClickRegistered?.Invoke(this, earning, eventData.pointerPressRaycast.worldPosition);
            ConfirmEarning(earning);
        }
    }
}
