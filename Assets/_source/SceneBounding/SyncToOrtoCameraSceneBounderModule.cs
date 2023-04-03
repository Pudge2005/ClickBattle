using UnityEngine;

namespace DevourNovelEngine.Prototype.Utils
{
    public sealed class SyncToOrtoCameraSceneBounderModule : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;
        [SerializeField] private Camera _cam;
        [SerializeField] private bool _useMainCam;

        private int _camW;
        private int _camH;


        private void Awake()
        {
            if (_useMainCam)
                _cam = Camera.main;

            _camW = _cam.pixelWidth;
            _camH = _cam.pixelHeight;
            SyncBounds();
        }


        private void Update()
        {
            if (CheckCamSize())
                SyncBounds();

            Vector3 min = new()
            {
                z = _distance
            };

            Vector3 max = Vector3.one;
            max.z = _distance;

            _min.position = _cam.ViewportToWorldPoint(min);
            _max.position = _cam.ViewportToWorldPoint(max);
        }

        private bool CheckCamSize()
        {
            int w = _cam.pixelWidth;
            int h = _cam.pixelHeight;

            bool changed = false;

            if (_camW != w)
            {
                _camW = w;
                changed = true;
            }

            if (_camH != h)
            {
                _camH = h;
                changed = true;
            }

            return changed;
        }

        private void SyncBounds()
        {
            Vector3 min = _cam.ViewportToWorldPoint(Vector3.zero);
            Vector3 max = _cam.ViewportToWorldPoint(Vector3.one);

            _min.position = min;
            _max.position = max;
        }

    }

}
