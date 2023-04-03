using System;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace Game.Clicks
{
    public sealed class ClickRegistrator : MonoBehaviour
    {
        [SerializeField] private NetworkGameManager _gm;


        [SerializeField] private SceneBounderBase _bounder;

        [SerializeField] private float _newClickableSpawnDelay = 0.3f;

        private float _spawnCD;


        private void Update()
        {
            CountDown();
        }

        private void CountDown()
        {
            if ((_spawnCD -= Time.deltaTime) > 0)
                return;


            //ResetCD();
        }

        private void ResetCD()
        {

        }

        internal void RegisterClick(ClickableTarget clickTarget)
        {
            _gm.RegisterPoints(clickTarget.PointsDelta);
        }

    }
}
