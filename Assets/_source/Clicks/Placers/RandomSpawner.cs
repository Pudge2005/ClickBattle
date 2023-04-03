using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace Game.Clicks
{
    public sealed class RandomSpawner : ClickablesSpawner
    {
        [SerializeField] private ClickableTarget[] _clickablePrefabs;
        [SerializeField] private SceneBounderBase _sceneBounder;

        private readonly System.Random _rng = new();


        protected override ClickableTarget Spawn()
        {
            var inst = RandomHelpers.RandomElement(_clickablePrefabs, _rng);
            inst.transform.position = _sceneBounder.GetRandomPoint();
            return inst;
        }
    }


    public sealed class PredefinedSpawner : ClickablesSpawner
    {
        protected override ClickableTarget Spawn()
        {
            throw new System.NotImplementedException();
        }
    }
}
