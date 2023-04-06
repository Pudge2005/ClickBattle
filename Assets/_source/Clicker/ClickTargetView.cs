using Game.Core;
using Game.Visual;
using UnityEngine;

namespace Game.Clicker
{
    [RequireComponent(typeof(ClickTarget))]
    public sealed class ClickTargetView : MonoBehaviour
    {
        [SerializeField] private PoppingText _popTextPrefab;
        [SerializeField] private Gradient _popTextGradient;
        [SerializeField] private Vector3 _minDir = new Vector3(-0.5f, 1f, 0f).normalized;
        [SerializeField] private Vector3 _maxDir = new Vector3(0.5f, 1f, 0f).normalized;

        private System.Random _directionRng = new();
        private ClickTarget _clickTarget;


        private void Awake()
        {
            _clickTarget = GetComponent<ClickTarget>();

            GameManager.ClickRegistered += HandleClickRegistered;
        }

        private void HandleClickRegistered(float bounty, Vector3 clickWorldPos)
        {
            var popInst = Instantiate(_popTextPrefab, clickWorldPos, Quaternion.identity);
            popInst.UseWorldFlyDirection = true;
            popInst.FlyDirection = Vector3.Lerp(_minDir, _maxDir, (float)_directionRng.NextDouble());
            popInst.Text = Mathf.RoundToInt(bounty).ToString();
        }
    }
}
