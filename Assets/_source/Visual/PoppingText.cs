using TMPro;
using UnityEngine;

namespace Game.Visual
{
    // torefactor (not during jam obv): props are ment to be changed only before Start,
    // so there should be Init(float? timeToLive = null, AnimationCurve sizeCurve = null...) and
    // props should be readonly so it is more intuitive and client-safe
    public sealed class PoppingText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMesh;
        [SerializeField] private float _timeToLive = 2f;
        [SerializeField] private AnimationCurve _sizeCurve;
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private AnimationCurve _transparencyCurve;
        [SerializeField] private Vector3 _flyDirection;
        [SerializeField] private bool _useWorldFlyDirection;

        private float _timeToLiveLeft;
        private Vector3 _initialScale;
        private float _initialTransparency;


        public float TimeToLive
        {
            get => _timeToLive;
            set => _timeToLive = value;
        }

        public AnimationCurve SizeCurve
        {
            get => _sizeCurve;
            set => _sizeCurve = value;
        }

        public AnimationCurve SpeedCurve
        {
            get => _speedCurve;
            set => _speedCurve = value;
        }

        public AnimationCurve TransparencyCurve
        {
            get => _transparencyCurve;
            set => _transparencyCurve = value;
        }

        public Vector3 FlyDirection
        {
            get => _flyDirection;
            set => _flyDirection = value.normalized;
        }

        public bool UseWorldFlyDirection
        {
            get => _useWorldFlyDirection;
            set => _useWorldFlyDirection = value;
        }


        public string Text
        {
            get => _textMesh.text;
            set => _textMesh.text = value;
        }

        public Color Color
        {
            get => _textMesh.color;
            set => _textMesh.color = value;
        }

        public float FontSize
        {
            get => _textMesh.fontSize;
            set => _textMesh.fontSize = value;
        }

        public TextAlignmentOptions Alignment
        {
            get => _textMesh.alignment;
            set => _textMesh.alignment = value;
        }

        public bool EnableAutoSizing
        {
            get => _textMesh.enableAutoSizing;
            set => _textMesh.enableAutoSizing = value;
        }


        private void Awake()
        {
            _flyDirection = _flyDirection.normalized;
        }

        private void Start()
        {
            InitInitialData();
            Prewarm();
        }

        private void Update()
        {
            Progress(Time.deltaTime);
        }


        public TextMeshPro GetInternalTextMeshPro()
        {
            return _textMesh;
        }


        private void InitInitialData()
        {
            _initialScale = transform.localScale;
            _initialTransparency = _textMesh.color.a;
        }

        private void Prewarm()
        {
            EvaluateAll(0);
        }

        private void Progress(float deltaTime)
        {
            _timeToLiveLeft -= deltaTime;

            float t = 1f - _timeToLiveLeft / _timeToLive;

            if (Mathf.Approximately(t, 0f))
                return;

            if (t >= 1f || float.IsSubnormal(t))
            {
                Destroy(gameObject);
                return;
            }

            EvaluateAll(t);
            Move(t, deltaTime);
        }

        private void EvaluateAll(float t)
        {
            EvaluateSize(t);
            EvaluateTransparency(t);
        }

        private void EvaluateTransparency(float t)
        {
            if (_transparencyCurve == null)
                return;

            float a = _initialTransparency * _transparencyCurve.Evaluate(t);
            var color = _textMesh.color;
            color.a = a;
            _textMesh.color = color;
        }

        private void EvaluateSize(float t)
        {
            if (_sizeCurve == null)
                return;

            transform.localScale = _initialScale * _sizeCurve.Evaluate(t);
        }

        private void Move(float t, float deltaTime)
        {
            float speed = _speedCurve.Evaluate(t);
            Vector3 direction = _flyDirection;

            if (!_useWorldFlyDirection)
                direction = transform.rotation * direction;

            transform.position += direction * (speed * deltaTime);
        }


    }
}
