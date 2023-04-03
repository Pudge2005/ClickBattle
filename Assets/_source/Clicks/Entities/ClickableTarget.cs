using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Clicks
{
    public sealed class ClickableTarget : MonoBehaviour, IPointerDownHandler
    {
        [System.Serializable]
        private sealed class Effect
        {
            [SerializeField] private GameObject _particles;
            [SerializeField] private AudioClip _sound;


            public void Act(Vector3 pos)
            {
                if (_particles != null)
                    _ = Instantiate(_particles, pos, Quaternion.identity);

                if (_sound != null)
                    AudioSource.PlayClipAtPoint(_sound, pos);
            }
        }


        [SerializeField] private int _pointsDelta;

        [SerializeField] private Effect _onSpawnFX;
        [SerializeField] private Effect _onHitFX;

        private ClickRegistrator _parent;


        public int PointsDelta => _pointsDelta;


        public event System.Action<ClickableTarget> Hitted;


        internal void Init(ClickRegistrator parent)
        {
            _parent = parent;
        }

        private void Start()
        {
            _onSpawnFX.Act(transform.position);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Hitted?.Invoke(this);
            _onHitFX.Act(transform.position);
            _parent.RegisterClick(this);
        }
    }
}
