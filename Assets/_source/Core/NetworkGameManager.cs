using Unity.Netcode;

namespace Game.Core
{
    public sealed class NetworkGameManager : NetworkBehaviour
    {
        public delegate void ProgressChangedDelegate(int newValue, int delta);


        private readonly NetworkVariable<int> _progress =
            new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


        public int Progress => _progress.Value;


        public event ProgressChangedDelegate ProgressChanged;


        private void Awake()
        {
            _progress.OnValueChanged += HandleProgressValueChanged;
        }


        public void RegisterPoints(int delta)
        {
            ChangeProgressServerRpc(delta);
        }


        [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
        private void ChangeProgressServerRpc(int delta)
        {
            _progress.Value += delta;
        }

        private void HandleProgressValueChanged(int previousValue, int newValue)
        {
            ProgressChanged?.Invoke(newValue, newValue - previousValue);
        }
    }
}
