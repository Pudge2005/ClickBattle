using System;
using Game.Modifiers;
using Game.Processors;
using UnityEngine;

namespace Game.Core
{
    public sealed class PassiveEarningManager : MonoBehaviour
    {
        [SerializeField] private EarnModifiersCollection _earnModifiersCollection;

    }


    [DefaultExecutionOrder(-1000)]
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void ClickRegisteredDelegate(float bounty, Vector3 clickWorldPosition);


        [SerializeField] private float _defaultClickReward = 10f;
        [SerializeField] private AntiAutoClicker _antiAutoClicker;
        [SerializeField] private EarnModifiersCollection _earnModifiersCollection;

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

        public static void RegisterClick(Vector3 clickWorldPos)
        {
            if (!_instance._antiAutoClicker.ShouldRegisterClick())
                return;

            RegisterClickInternal(clickWorldPos);
        }


        private static void RegisterClickInternal(Vector3 clickWorldPos)
        {
            float bounty = CalculateClickReward();
            _instance.ClickRegisteredInternal?.Invoke(bounty, clickWorldPos);
        }

        private static float CalculateClickReward()
        {
            return _instance._earnModifiersCollection.pro
        }
    }
}
