using System;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{
    public sealed class SplittedBounder2D : SceneBounderBase
    {
        [SerializeField] private SceneBounderBase _bounder;
        [SerializeField] private bool _splitHorizontal = true;
        [Tooltip("Ratio of THIS part (TakeSecondPart is taken in the account)")]
        [SerializeField, Range(0f, 1f)] private float _ratio = 0.5f;
        [Tooltip("if SplitHorizontal: use lower part, else: right part")]
        [SerializeField] private bool _takeSecondPart;


        public override Vector3 Min
        {
            get
            {
                var min = _bounder.Min;
                var max = _bounder.Max;

                Vector3 splittedMin = min;

                if (_splitHorizontal)
                {
                    splittedMin.x = min.x;

                    if (!_takeSecondPart)
                    {
                        float height = (max.y - min.y) * _ratio;
                        splittedMin.y = max.y - height;
                    }
                    else
                    {
                        splittedMin.y = min.y;
                    }
                }
                else
                {
                    //throw new NotImplementedException($"min point for vertically splitted bounder is not implemented");

                    splittedMin.y = min.y;

                    if (_takeSecondPart)
                    {
                        float width = (max.x - min.x) * _ratio;
                        splittedMin.x = max.x - width;
                    }
                    else
                    {
                        splittedMin.x = min.x;
                    }
                }

                return splittedMin;
            }
        }

        public override Vector3 Max
        {
            get
            {
                var min = _bounder.Min;
                var max = _bounder.Max;

                Vector3 splittedMax = max;

                if (_splitHorizontal)
                {
                    splittedMax.x = max.x;

                    if (!_takeSecondPart)
                    {
                        splittedMax.y = max.y;
                    }
                    else
                    {
                        float height = (max.y - min.y) * (1f - _ratio);
                        splittedMax.y = max.y - height;
                    }
                }
                else
                {
                    //throw new NotImplementedException($"max point for vertically splitted bounder is not implemented");

                    splittedMax.y = max.y;

                    if (_takeSecondPart)
                    {
                        splittedMax.x = max.x;
                    }
                    else
                    {
                        float width = (max.x - min.x) * (1f - _ratio);
                        splittedMax.x = max.x - width;
                    }
                }

                return splittedMax;
            }
        }
    }

}
