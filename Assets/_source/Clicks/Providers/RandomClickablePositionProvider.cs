using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace Game.Clicks
{
    [System.Serializable]
    public sealed class RandomClickablePositionProvider : IClickablePositionProvider
    {
        [SerializeField] private SceneBounderBase _bounder;


        public Vector3 GetPosition()
        {
            return _bounder.GetRandomPoint();
        }
    }
}
