using System.Collections.Generic;
using Game.Processors;
using Game.Upgrades;
using UnityEngine;

namespace Game.Core
{
    public sealed class PassiveEarningManager : MonoBehaviour
    {

    }

    public sealed class ActiveEarningManager : MonoBehaviour
    {
        private List<ActiveUpgradeData>
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
}
