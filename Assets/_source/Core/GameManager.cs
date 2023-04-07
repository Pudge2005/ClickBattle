using UnityEngine;

namespace Game.Core
{
    [DefaultExecutionOrder(-1000)]
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void BalanceChangedDelegate(GameManager gameManager, float newBalance, float delta);


        private static GameManager _instance;

        private INetworkGameManager _netGM;
        private float _winBalance;
        private float _balance;


        public static INetworkGameManager NetworkGameManager => _instance._netGM;
        public static float WinBalance => _instance._winBalance;
        public static float Balance => _instance._balance;


        private event System.Action<INetworkGameManager> NetworkGameManagerInitedInternal;

        private event BalanceChangedDelegate BalanceChangedInternal;



        public static event System.Action<INetworkGameManager> NetworkGameManagerInited
        {
            add => _instance.NetworkGameManagerInitedInternal += value;
            remove => _instance.NetworkGameManagerInitedInternal -= value;
        }


        public static event BalanceChangedDelegate BalanceChanged
        {
            add => _instance.BalanceChangedInternal += value;
            remove => _instance.BalanceChangedInternal -= value;
        }


        private void Awake()
        {
            _instance = this;
        }


        public void Init(INetworkGameManager networkGameManager, float winBalance)
        {
            _netGM = networkGameManager;
            _winBalance = winBalance;
            NetworkGameManagerInitedInternal?.Invoke(networkGameManager);
        }


        public static bool CanSpend(float value)
        {
            return _instance._balance - value >= 0;
        }

        public static bool TrySpend(float value)
        {
            if (CanSpend(value))
            {
                Spend(value);
                return true;
            }

            return false;
        }

        public static void Spend(float value)
        {
            if (value < 0)
            {
#if UNITY_EDITOR
                throw new System.ArgumentException($"value should not be negative: {value}");
#else
                return;
#endif
            }


            ChangeBalance(-value);
        }

        public static void Earn(float earning)
        {
            ChangeBalance(earning);
        }

        private static void ChangeBalance(float delta)
        {
            var inst = _instance;
            ref var balance = ref inst._balance;
            balance += delta;
            inst._netGM.SetPlayerBalance(balance);
            inst.BalanceChangedInternal?.Invoke(inst, balance, delta);
        }

    }
}
