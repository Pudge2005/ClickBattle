using System;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Clicks
{

    public abstract class ClickablesSpawner : MonoBehaviour
    {
        [SerializeField] private ClampedCountClickablesPlacer _placer;


        protected virtual void Awake()
        {
            _placer.Init(Spawn);
        }


        protected abstract ClickableTarget Spawn();
    }


    public sealed class ClampedCountClickablesPlacer : ClickablesPlacerBase
    {
        [SerializeField] private int _minCount;
        [SerializeField] private int _maxCount;
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;

        private System.Random _rng;
        private int _currentCount;
        private float _spawnCD;

        private Func<ClickableTarget> _spawnFunc;


        private void Awake()
        {
            _rng = new();
            enabled = false;
        }


        public void Init(Func<ClickableTarget> spawnFunc)
        {
            _spawnFunc = spawnFunc;
            enabled = true;
        }


        private void Update()
        {
            if (_currentCount >= _maxCount)
                return;

            while (_currentCount < _minCount)
            {
                SpawnInternal();
            }

            CountDown(Time.deltaTime);
        }


        private void CountDown(float deltaTime)
        {
            if ((_spawnCD -= deltaTime) > 0)
                return;

            ResetCD();
            SpawnInternal();
        }

        private void ResetCD()
        {
            _spawnCD = RandomHelpers.NextFloat(_minSpawnDelay, _maxSpawnDelay, _rng);
        }


        private void SpawnInternal()
        {
            ++_currentCount;
            var inst = _spawnFunc();
            inst.Hitted += HandleClickableHitted;
            InitClickable(inst);
        }

        private void HandleClickableHitted(ClickableTarget clickable)
        {
            // if pooling
            //clickable.Hitted -= HandleClickableHitted;

            --_currentCount;

            // не спавним новые кликаблы из
            // этого контекста, т.к. это стек
            // эвента
        }
    }
}
