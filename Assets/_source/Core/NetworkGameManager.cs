using Unity.Netcode;
using UnityEngine;

namespace Game.Core
{
    [DefaultExecutionOrder(-9999)]
    public sealed class NetworkGameManager : NetworkBehaviour
    {
        public delegate void ProgressChangedDelegate(int newValue, int delta);


        private readonly NetworkVariable<int> _progress =
            new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


        private static NetworkGameManager _instance;


        public static int Progress => _instance._progress.Value;


        private event ProgressChangedDelegate ProgressChangedInternal;


        public static event ProgressChangedDelegate ProgressChanged
        {
            add => _instance.ProgressChangedInternal += value;
            remove => _instance.ProgressChangedInternal -= value;
        }


        private void Awake()
        {
            _instance = this;
            _progress.OnValueChanged += HandleProgressValueChanged;
        }


        public static void RegisterPoints(int delta)
        {
            _instance.ChangeProgressServerRpc(delta);
        }


        [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
        private void ChangeProgressServerRpc(int delta)
        {
            _progress.Value += delta;
        }

        private void HandleProgressValueChanged(int previousValue, int newValue)
        {
            ProgressChangedInternal?.Invoke(newValue, newValue - previousValue);
        }
    }
}
