using UnityEngine;

namespace Game.Clicks
{
    public sealed class ClickRegistrator : MonoBehaviour
    {
        [SerializeField] private NetworkGameManager _gm;


        internal void RegisterClick(ClickableTarget clickTarget)
        {
            _gm.RegisterPoints(clickTarget.PointsDelta);
        }

    }
}
