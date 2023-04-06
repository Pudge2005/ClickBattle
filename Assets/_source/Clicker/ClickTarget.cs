using Game.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Clicker
{
    public sealed class ClickTarget : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.RegisterClick(eventData.pointerPressRaycast.worldPosition);
        }
    }
}
