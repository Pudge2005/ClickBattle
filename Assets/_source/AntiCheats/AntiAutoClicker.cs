using UnityEngine;

namespace Game.AntiCheats
{
    public sealed class AntiAutoClicker : MonoBehaviour
    {
        [SerializeField] private float _maxClicksPerSecond = 14f;

        private float _minDelta;
        private float _blockCD;


        private void Awake()
        {
            _minDelta = 1f / _maxClicksPerSecond;
        }


        private void Update()
        {
            _blockCD -= Time.deltaTime;
        }

        public bool ShouldRegisterClick()
        {
            if (_blockCD > 0)
                return false;

            _blockCD = _minDelta;
            return true;
        }
    }
}
