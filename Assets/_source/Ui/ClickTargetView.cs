using Game.Earners.Clicker;
using Game.Visual;
using UnityEngine;

namespace Game.Ui
{
    [RequireComponent(typeof(ClickTarget))]
    public sealed class ClickTargetView : MonoBehaviour
    {
        [SerializeField] private PoppingText _popTextPrefab;
        [SerializeField] private Gradient _popTextGradient;
        [SerializeField] private Vector3 _minDir = new Vector3(-0.5f, 1f, 0f).normalized;
        [SerializeField] private Vector3 _maxDir = new Vector3(0.5f, 1f, 0f).normalized;

        private readonly System.Random _directionRng = new();
        private ClickTarget _clickTarget;


        private void Awake()
        {
            _clickTarget = GetComponent<ClickTarget>();
            _clickTarget.ClickRegistered += HandleClickRegistered;
        }


        private void HandleClickRegistered(ClickTarget click, float bounty, Vector3 clickWorldPosition)
        {
            var popInst = Instantiate(_popTextPrefab, clickWorldPosition, Quaternion.identity);
            popInst.UseWorldFlyDirection = true;
            popInst.FlyDirection = Vector3.Lerp(_minDir, _maxDir, (float)_directionRng.NextDouble());
            popInst.Text = Mathf.RoundToInt(bounty).ToString();
        }
    }
}
