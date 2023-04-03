using UnityEngine;

namespace Game.Clicks
{
    public abstract class ClickablesManagerBase : MonoBehaviour, IClickablePrefabProvider, IClickablePositionProvider
    {
        public abstract ClickableTarget GetPrefab();
        public abstract Vector3 GetPosition();
    }
}
