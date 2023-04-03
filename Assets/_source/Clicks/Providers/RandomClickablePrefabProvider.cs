using UnityEngine;

namespace Game.Clicks
{
    [System.Serializable]
    public sealed class RandomClickablePrefabProvider : IClickablePrefabProvider
    {
        [SerializeField] private ClickableTarget[] _prefabs;


        public ClickableTarget GetPrefab()
        {
            return _prefabs[UnityEngine.Random.Range(0, _prefabs.Length)];
        }
    }
}
