using System;
using System.Runtime.CompilerServices;
using Game.Modifiers;
using Game.Processors;
using UnityEngine;

namespace Game.Core
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void BalanceChangedDelegate(GameManager gameManager, float newBalance, float delta);


        private static GameManager _instance;

        private INetworkGameManager _netGM;
        private float _balance;


        public static float Balance => _instance._balance;


        private event BalanceChangedDelegate BalanceChangedInternal;


        public static event BalanceChangedDelegate BalanceChanged
        {
            add => _instance.BalanceChangedInternal += value;
            remove => _instance.BalanceChangedInternal -= value;
        }


        private void Awake()
        {
            _instance = this;
        }


        public void Init(INetworkGameManager networkGameManager)
        {
            _netGM = networkGameManager;
        }


        public static void RegisterEarning(float earning)
        {
            var inst = _instance;
            ref var balance = ref inst._balance;
            balance += earning;
            inst._netGM.SetPlayerScore(balance);
            inst.BalanceChangedInternal?.Invoke(inst, balance, earning);
        }


    }
}
