using System.Collections.Generic;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Clicks
{
    public sealed class PopItClickablesManager : ClickablesManagerBase
    {
        


        private void Awake()
        {
            
        }

        public override ClickableTarget GetPrefab()
        {
           
        }

        public override Vector3 GetPosition()
        {
        }
    }


    [System.Serializable]
    public sealed class FixedClickablesProvider : IClickablePrefabProvider, IClickablePositionProvider
    {
        [SerializeField] private float _activeFromStartClickablesRatio = 0.3f;
        [SerializeField] private ClickableTarget[] _predefinedClickables;

        private readonly List<int> _activeClickableIndices = new();
        private readonly List<int> _inactiveClickableIndices = new();

        private readonly System.Random _rng = new();


        public ClickableTarget GetPrefab()
        {
            var index = GetRandomActiveClickableIndex();
            var prefab =

        }

        public Vector3 GetPosition()
        {
            throw new System.NotImplementedException();
        }




        private int GetRandomActiveClickableIndex()
        {
            return RandomHelpers.RandomIndex(_activeClickableIndices, _rng);
        }
        
        private int GetRandomInactiveIndex()
        {
            return RandomHelpers.RandomIndex(_inactiveClickableIndices, _rng);

        }

    }
}
