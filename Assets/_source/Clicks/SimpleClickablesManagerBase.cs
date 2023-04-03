using UnityEngine;

namespace Game.Clicks
{
    public abstract class SimpleClickablesManagerBase<TPrefabProvider, TPosProvider> : ClickablesManagerBase
        where TPrefabProvider : IClickablePrefabProvider
        where TPosProvider : IClickablePositionProvider
    {
        [SerializeField] private TPrefabProvider _prefabProvider;
        [SerializeField] private TPosProvider _posProvider;


        public sealed override ClickableTarget GetPrefab() => _prefabProvider.GetPrefab();

        public sealed override Vector3 GetPosition() => _posProvider.GetPosition();
    }
}
