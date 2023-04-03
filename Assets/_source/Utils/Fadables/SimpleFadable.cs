using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Fadables
{
    public sealed class SimpleFadable : Fadable
    {
        [SerializeField] private Renderer[] _renderers;

        private readonly List<Material> _mats = new();


        private void Awake()
        {
            foreach (var renderer in _renderers)
            {
                var mats = renderer.materials;
                _mats.AddRange(mats);
                //renderer.materials = mats;
            }
        }


        protected override void SetAlpha(float value)
        {
            foreach (var m in _mats)
            {
                var c = m.color;
                c.a = value;
                m.color = c;
            }
        }
    }
}
