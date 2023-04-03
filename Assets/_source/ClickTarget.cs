using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Game.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public sealed class ClickTarget : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {

        }
    }

    [DefaultExecutionOrder(-1000)]
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void ClickRegisteredDelegate(float bounty);


        [SerializeField] private AntiAutoClicker _antiAutoClicker;

        private INetworkGameManager _netGM;

        private static GameManager _instance;


        private event ClickRegisteredDelegate ClickRegisteredInternal;


        public static event ClickRegisteredDelegate ClickRegistered
        {
            add => _instance.ClickRegisteredInternal += value;
            remove => _instance.ClickRegisteredInternal -= value;
        }


        private void Awake()
        {
            _instance = this;
        }


        public void Init(INetworkGameManager networkGameManager)
        {
            _netGM = networkGameManager;
        }

        public static void RegisterClick()
        {
            if (!_instance._antiAutoClicker.ShouldRegisterClick())
                return;

            RegisterClickInternal();
        }


        private static void RegisterClickInternal()
        {

        }
    }

    public sealed class ActiveUpgradeData
    {
        private readonly ActiveUpgradeSo _upgrade;
        private int _level;


        /// <param name="level">INDEX, DO NOT START FROM 1</param>
        public ActiveUpgradeData(ActiveUpgradeSo upgrade, int level = 0)
        {
            _upgrade = upgrade;
            _level = level;
        }


        public ActiveUpgradeSo Upgrade => _upgrade;
        public int Level => _level;


        public void SetLevel(int newLevel)
        {
            _level = newLevel;
        }


        public override int GetHashCode()
        {
            return _upgrade.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ActiveUpgradeData other
                && other.GetHashCode() == GetHashCode();
        }
    }

    public sealed class ActiveUpgradeAffectorData
    {
        private readonly ActiveUpgradeAffectorSo _affector;
        private int _level;


        /// <param name="level">INDEX, DO NOT START FROM 1</param>
        public ActiveUpgradeAffectorData(ActiveUpgradeAffectorSo affector, int level = 0)
        {
            _affector = affector;
            _level = level;
        }


        public ActiveUpgradeAffectorSo Affector => _affector;
        public int Level => _level;


        public void SetLevel(int newLevel)
        {
            _level = newLevel;
        }


        public override int GetHashCode()
        {
            return _affector.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ActiveUpgradeAffectorData other
                && other.GetHashCode() == GetHashCode();
        }
    }

    public sealed class UpgradesCollection : MonoBehaviour
    {
        // so, level

        private readonly Dictionary<ActiveUpgradeSo, ActiveUpgradeData> _activeUpgrades = new();

        // todo: passive upgrades

        //private readonly Dictionary<ActiveUpgradeSo, ActiveUpgradeAffectorData> _activeUpgradesAffectors = new();

        private readonly Dictionary<ActiveUpgradeSo, HashSet<ActiveUpgradeAffectorData>> _activeUpgradesAffectors = new();


        //public bool TryGetActiveUpgradeData(ActiveUpgradeSo upgrade, out ActiveUpgradeData data)
        //{
        //    return _activeUpgrades.TryGetValue(upgrade, out data);
        //}

        //public bool TryGetActiveUpgradeAffectorData(ActiveUpgradeSo upgrade, out ActiveUpgradeAffectorData data)
        //{
        //    return _activeUpgradesAffectors.TryGetValue(upgrade, out data);
        //}

        //public void SetActiveUpgradeLevel(ActiveUpgradeSo upgrade, int level)
        //{
        //    if(!TryGetActiveUpgradeData(upgrade, out var data))
        //    {
        //        AddActiveUpgrade(upgrade, level);
        //        return;
        //    }

        //    data.SetLevel(level);
        //}

        //public void AddActiveUpgrade(ActiveUpgradeSo upgradeSo, int level = 0)
        //{
        //    _activeUpgrades.Add(upgradeSo, new ActiveUpgradeData(upgradeSo, level));
        //}

        //public void RemoveActiveUpgrade(ActiveUpgradeSo upgrade)
        //{
        //    _ = _activeUpgrades.Remove(upgrade);
        //}

        //public void RemoveActiveUpgradeAffector(ActiveUpgradeSo upgrade)
        //{

        //}
    }

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




    [CreateAssetMenu(menuName = "Game/Active Upgrade")]
    public sealed class ActiveUpgradeSo : SoDatabaseElement
    {
        [System.Serializable]
        private sealed class Level
        {
            [SerializeField, Min(0f)] private float _flatDelta;
            [SerializeField, Min(1f)] private float _multiplier = 1f;


            public float Process(float source)
            {
                return source * _multiplier + _flatDelta;
            }
        }


        [SerializeField] private Level[] _levels;


        public float Process(float source, int upgradeLvl)
        {
            return _levels[upgradeLvl].Process(source);
        }
    }


    [CreateAssetMenu(menuName = "Game/Active Upgrade Penalty")]
    public sealed class ActiveUpgradeAffectorSo : SoDatabaseElement
    {
        [System.Serializable]
        private sealed class Level
        {
            [SerializeField] private float _flatDelta;
            [SerializeField, Min(0f)] private float _multiplier = 1f;


            public float Process(float source)
            {
                return source * _multiplier + _flatDelta;
            }
        }


        [SerializeField] private ActiveUpgradeSo _affectedUpgrade;
        [SerializeField] private Level[] _levels;


        public float Process(float source, int upgradeLvl)
        {
            return _levels[upgradeLvl].Process(source);
        }
    }


    public interface INetworkGameManager
    {
        public delegate void ScoreChangedDelegate(float newValue);
        public delegate void ScoreChangedWithDeltaDelegate(float newValue, float delta);

        public delegate void UpgradeAffectorAppliedDelegate(ActiveUpgradeAffectorSo affector, int lvl, float duration);


        float PlayerScore { get; }
        float OpponentScore { get; }



        // ƒл€ клиента нет совершенно никакого смысла в этом событии,
        // так как он сам прекрасно знает, сколько у него очков, так
        // еще и без задержек, так еще и с правильными дельтами, ведь
        // он обрабатыват клики на своей стороне.
        // ≈сли будет введена возможность получени€ бонусных очков
        // "извне" (по логике сервера), то вместо синхронизации
        // NetworkVariable, клиенту нужно получить от сервера дельту
        // бонуса. 

        //event ScoreChangedDelegate PlayerScoreChanged;

        // ћы не можем получить насто€щую дельту из NetworkVariable,
        // мы получаем только разницу между "снимками", то есть,
        // если тикрейт сервера 1 тик в секунду, а оппонент кликает
        // 10 раз в секунду, получа€ по 2 очка за каждый клик, то
        // мы каждую секунду будем получать эвент с дельтой в 20,
        // в чем нет совершенно никакого смысла.

        event ScoreChangedDelegate OpponentScoreChanged;

        event UpgradeAffectorAppliedDelegate UpgradeAffectorApplied;


        void ChangePlayerScore(float delta);
    }

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
