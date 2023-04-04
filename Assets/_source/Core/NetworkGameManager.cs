using Unity.Netcode;

namespace Game
{
    public class NetworkGameManager : NetworkBehaviour, INetworkGameManager
    {
        private readonly NetworkVariable<float> _p0Score = InitNetVar();
        private readonly NetworkVariable<float> _p1Score = InitNetVar();

        private int _playerIndex = -1;


        public float PlayerScore => GetPlayerScoreNetVar(PlayerIndex).Value;

        public float OpponentScore => GetPlayerScoreNetVar(OpponentIndex).Value;


        internal int PlayerIndex => _playerIndex;
        internal int OpponentIndex => _playerIndex == 0 ? 1 : 0;


        public event INetworkGameManager.ScoreChangedDelegate OpponentScoreChanged;
        public event INetworkGameManager.UpgradeAffectorAppliedDelegate UpgradeAffectorApplied;

        public void ChangePlayerScore(float delta)
        {
            GetPlayerScoreNetVar(_playerIndex).Value += delta;
        }

        private NetworkVariable<float> GetPlayerScoreNetVar(int playerIndex)
        {
            return playerIndex switch
            {
                0 => _p0Score,
                1 => _p1Score,
                _ => throw new System.Exception($"index: {playerIndex}"),
            };
        }


        private static NetworkVariable<float> InitNetVar()
        {
            return new NetworkVariable<float>(0f,
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Server);
        }
    }
}
