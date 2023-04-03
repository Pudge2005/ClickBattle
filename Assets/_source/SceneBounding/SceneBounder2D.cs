using DevourDev.Unity.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{
    public sealed class SceneBounder2D : SceneBounderBase
    {

        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;


        public override Vector3 Min => _min.position;
        public override Vector3 Max
        {
            get
            {
                Vector3 max = _max.position;
                max.z = Min.z;
                return max;
            }
        }

        internal Transform MinTransform => _min;
        internal Transform MaxTransform => _max;


        protected override Vector3 GetRandomPointInternal(System.Random rng)
        {
            var min = Min;
            var max = Max;

            Vector3 point;

            point.x = RandomHelpers.NextFloat(min.x, max.x, rng);
            point.y = RandomHelpers.NextFloat(min.y, max.y, rng);
            point.z = min.z;

            return point;
        }
    }

}
