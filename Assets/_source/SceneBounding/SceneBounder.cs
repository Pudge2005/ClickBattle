using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{
    public sealed class SceneBounder : SceneBounderBase
    {
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;


        public override Vector3 Min => _min.position;
        public override Vector3 Max => _max.position;


        internal Transform MinTransform => _min;
        internal Transform MaxTransform => _max;

    }

}
