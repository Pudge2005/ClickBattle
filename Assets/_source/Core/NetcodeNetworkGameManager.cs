using System.Collections.Generic;
using Game.Earners;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Game.Core
{
    public sealed class NetcodeNetworkGameManager : NetworkBehaviour, INetworkGameManager
    {
        [SerializeField] private float _winBalance;
        [SerializeField] private EarnersDatabaseSo _earnersDatabase;


        private readonly NetworkVariable<float> _winBalanceNetVar =
            new(-1f, readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Server);


        private readonly NetworkVariable<float> _p0BalanceNetVar =
            new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Owner);

        private readonly NetworkVariable<float> _p1BalanceNetVar =
            new(readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Owner);


        private readonly Dictionary<EarnerSo, int> _earnerNegLvls = new();

        private NativeArray<ulong> _toP0;
        private NativeArray<ulong> _toP1;


        public float WinBalance
        {
            get
            {
                if (_winBalanceNetVar.Value < 0)
                    return _winBalance;

                return _winBalanceNetVar.Value;
            }
        }

        public float OpponentBalance => GetOpponentBalanceNetVar().Value;


        public IReadOnlyDictionary<EarnerSo, int> EarnerNegativeLevels => _earnerNegLvls;


        private int PlayerID => (int)OwnerClientId;


        public event INetworkGameManager.BalanceChangedDelegate OpponentBalanceChanged;
        public event INetworkGameManager.EarnerNegativeLevelChangedDelegate EarnerNegativeLevelChanged;
        public event INetworkGameManager.GameEndedDelegate GameEnded;


        private void Awake()
        {
            _toP0 = new(new ulong[] { 0 }, Allocator.Persistent);
            _toP1 = new(new ulong[] { 1 }, Allocator.Persistent);

            _winBalanceNetVar.Value = _winBalance;
        }

        public override void OnDestroy()
        {
            if (_toP0 != null)
                _toP0.Dispose();

            if (_toP1 != null)
                _toP1.Dispose();
        }

        public void SetPlayerBalance(float playerBalance)
        {
            GetPlayerBalanceNetVar().Value = playerBalance;

            if (playerBalance >= WinBalance)
            {
                AnnounceAsWinnerServerRpc();
            }
        }

        public void SetOpponentEarnerNegativeLevel(EarnerSo earner, int lvl)
        {
            SetOpponentEarnerNegativeLevelServerRpc(earner.GetDatabaseId(), lvl);
        }


        [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
        private void AnnounceAsWinnerServerRpc(ServerRpcParams serverRpcParams = default)
        {
            AnnounceWinnerClientRpc((byte)serverRpcParams.Receive.SenderClientId);
        }

        [ClientRpc(Delivery = RpcDelivery.Reliable)]
        private void AnnounceWinnerClientRpc(byte winnerID)
        {
            bool isWinner = (int)winnerID == PlayerID;
            GameEnded?.Invoke(isWinner);
        }


        [ServerRpc(Delivery = RpcDelivery.Reliable, RequireOwnership = false)]
        private void SetOpponentEarnerNegativeLevelServerRpc(int earnerSoId, int lvl, ServerRpcParams serverRpcParams = default)
        {
            var receiver = serverRpcParams.Receive.SenderClientId == 0 ? _toP1 : _toP0;

            var clientRpcParams = new ClientRpcParams()
            {
                Send = new ClientRpcSendParams()
                {
                    TargetClientIdsNativeArray = receiver,
                },
            };

            SetEarnerNegativeLevelClientRpc(earnerSoId, lvl, clientRpcParams);
        }


        [ClientRpc(Delivery = RpcDelivery.Reliable)]
        private void SetEarnerNegativeLevelClientRpc(int earnerSoId, int lvl, ClientRpcParams clientRpcParams = default)
        {
            Debug.Log($"SetEarnerNegativeLevelClientRpc received: soId: {earnerSoId}, lvl: {lvl}");

            var earnerSo = _earnersDatabase.GetElement(earnerSoId);

            if (_earnerNegLvls.ContainsKey(earnerSo))
                _earnerNegLvls[earnerSo] = lvl;
            else
                _earnerNegLvls.Add(earnerSo, lvl);

            EarnerNegativeLevelChanged?.Invoke(earnerSo, lvl);
        }


        private NetworkVariable<float> GetPlayerBalanceNetVar()
        {
            return PlayerID == 0 ? _p0BalanceNetVar : _p1BalanceNetVar;
        }

        private NetworkVariable<float> GetOpponentBalanceNetVar()
        {
            return PlayerID == 1 ? _p0BalanceNetVar : _p1BalanceNetVar;
        }

    }
}
