using Game.Core;
using UnityEngine;

namespace Game.Miscs
{
    [DefaultExecutionOrder(-999)]
    internal sealed class GameManagerInitor : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _networkGameManager;
        [SerializeField] private float _winBalance = 50_000f;
        [SerializeField, HideInInspector] private Component _netGmComp;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_networkGameManager != null
                && !ReferenceEquals(_networkGameManager, _netGmComp))
            {
                if (!_networkGameManager.TryGetComponent(typeof(INetworkGameManager), out var ngm))
                {
                    Debug.LogError($"Unable to find {nameof(INetworkGameManager)} component" +
                        $" on {_networkGameManager.name} GameObject");

                    _networkGameManager = null;
                }
                else
                {
                    _netGmComp = ngm;
                }

                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif


        private void Awake()
        {
            _gameManager.Init((INetworkGameManager)_netGmComp, _winBalance);
        }
    }
}
