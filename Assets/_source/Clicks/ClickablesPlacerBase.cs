using UnityEngine;

namespace Game.Clicks
{
    public abstract class ClickablesPlacerBase : MonoBehaviour
    {
        [SerializeField] private ClickRegistrator _clicksRegistrator;

        private readonly System.Random _rng = new();


        protected ClickableTarget CreateInstance()
        {
            var prefab = GetClickablePrefab();
            var inst = Instantiate(prefab);
            inst.Init(_clicksRegistrator);
            return inst;
        }


        private ClickableTarget GetClickablePrefab()
        {
            int index = _rng.Next(_clickTargetPrefabs.Length);
            return _clickTargetPrefabs[index];
        }
    }
}
