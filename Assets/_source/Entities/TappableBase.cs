using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Entities
{
    public abstract class TappableBase : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Collider[] _colliders;


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            HandleHit(eventData.pointerPressRaycast.worldPosition);
        }


        protected void SetCollidersState(bool enabledState)
        {
            foreach (var col in _colliders)
            {
                col.enabled = enabledState;
            }
        }


        protected abstract void HandleHit(Vector3 worldPosition);
    }


    [RequireComponent(typeof(PopItCell))]
    public sealed class PopItCellView : MonoBehaviour
    {
        [SerializeField] private ClickableEffect _poppedInEffect;
        [SerializeField] private ClickableEffect _poppedOutEffect;

        private PopItCell _popItCell;


        private void Awake()
        {
            _popItCell = GetComponent<PopItCell>();
            _popItCell.PoppedInStateChanged += HandlePoppedInStateChanged;
        }

        private void HandlePoppedInStateChanged(PopItCell cell, bool isPoppedIn)
        {
            var effect = isPoppedIn ? _poppedInEffect : _poppedOutEffect;
            effect.Act(transform.position);
        }
    }


    public sealed class PopItCell : TappableBase
    {
        private bool _isPoppedIn;


        public bool IsPoppedIn
        {
            get => _isPoppedIn;

            set
            {
                if (_isPoppedIn == value)
                    return;

                _isPoppedIn = value;

                HandlePoppedInStateChangedInternal(value);
                PoppedInStateChanged?.Invoke(this, value);
            }
        }


        public event System.Action<PopItCell, bool> PoppedInStateChanged;


        protected override void HandleHit(Vector3 worldPosition)
        {
            
        }


        private void HandlePoppedInStateChangedInternal(bool value)
        {
            SetCollidersState(value);
        }
    }
}
