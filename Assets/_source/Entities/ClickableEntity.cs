using System.Collections;
using Game.Utils.Fadables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Entities
{
    public abstract class ClickableEntity : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Collider _collider;


        protected Collider Collider => _collider;


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            HandleHit(eventData.pointerPressRaycast.worldPosition);
        }


        protected abstract void HandleHit(Vector3 worldPosition);
    }


    public sealed class NailClickable : ClickableEntity
    {
        [SerializeField] private GameObject _activeVisual;
        [SerializeField] private Fadable _inactiveVisual;

        [SerializeField] private float _fadeDelay = 1.6f;
        [SerializeField] private float _fadeTime = 0.8f;

        [SerializeField] private ClickableEffect _onBecameActiveEffect;
        [SerializeField] private ClickableEffect _onBecameInactiveEffect;


        protected override void HandleHit(Vector3 worldPosition)
        {
            Destroy(Collider);

            FadeDelayed();
        }


        public void FadeDelayed()
        {
            var enumerator = GetFadeDelayedEnumerator();
            _ = StartCoroutine(enumerator);
        }

        private IEnumerator GetFadeDelayedEnumerator()
        {
            yield return new WaitForSeconds(_fadeDelay);
            _inactiveVisual.Fade(_fadeTime);
        }
    }


    public class Clickable2 : ClickableEntity
    {
        [SerializeField] private GameObject _activeVisual;
        [SerializeField] private GameObject _inactiveVisual;

        [SerializeField] private ClickableEffect _onBecameActiveEffect;
        [SerializeField] private ClickableEffect _onBecameInactiveEffect;

        private bool _isActive;



        protected override void HandleHit(Vector3 position)
        {
            if (!_isActive)
            {
                Debug.LogError("unexpected behaviour");
                return;
            }

            Collider.enabled = false;

            //нужно хендлить отдельно, а вызывать только SetActive/SetInactive (не юнити) (или даже юнити)
        }
    }

    public sealed class PopItClickable : ClickableEntity
    {
        [SerializeField] private GameObject _popedInVisual;
        [SerializeField] private GameObject _popedOutVisual;

        [SerializeField] private ClickableEffect _onPopedInEffect;
        [SerializeField] private ClickableEffect _onPopedOutEffect;


        private GameObject _activeVisual;


        private void Awake()
        {
            _popedInVisual.SetActive(false);
            _popedOutVisual.SetActive(false);
        }

        public void InitPopItClickable(bool popedIn)
        {
            _activeVisual = popedIn ? _popedInVisual : _popedOutVisual;
            _activeVisual.SetActive(true);
        }

        protected override void HandleHit(Vector3 worldPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
