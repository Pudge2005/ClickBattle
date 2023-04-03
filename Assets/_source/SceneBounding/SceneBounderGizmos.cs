using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DevourNovelEngine.Prototype.Utils
{
    public class SceneBounderGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private SceneBounderBase _bounder;
        [SerializeField] private Color _color;
        [SerializeField] private bool _twoDiMode = true;
        [SerializeField] private float _thickness = 5f;
        [SerializeField] private bool _dotted;
        [SerializeField] private float _gapsSize = 5f;


        protected SceneBounderBase Bounder => _bounder;
        protected bool TwoDiMode => _twoDiMode;


        protected virtual void OnDrawGizmos()
        {
            if (_bounder == null)
                return;

            var min = _bounder.Min;
            var max = _bounder.Max;
            Vector3[] lineSegments;

            Vector3 ll = min;
            Vector3 ur = max;
            ur.z = ll.z;
            Vector3 ul = new(ll.x, ur.y, ll.z);
            Vector3 lr = new(ur.x, ll.y, ll.z);

            if (_twoDiMode)
            {
                lineSegments = new Vector3[] { ll, ul, ul, ur, ur, lr, lr, ll };
            }
            else
            {
                //"b" for Back

                Vector3 bll = ll;
                Vector3 bur = ur;
                Vector3 bul = ul;
                Vector3 blr = lr;

                bll.z = bur.z = bul.z = blr.z = max.z;

                lineSegments = new Vector3[]
                {
                    ll, ul, ul, ur, ur, lr, lr, ll,
                    ll, bll, ur, bur, ul, bul, lr, blr,
                    bll, bul, bul, bur, bur, blr, blr, bll
                };
            }

            DrawLines(lineSegments);
        }


        protected void DrawLines(Vector3[] lineSegments)
        {
            Handles.color = _color;

            for (int i = 0; i < lineSegments.Length; i += 2)
            {
                Vector3 a = lineSegments[i];
                Vector3 b = lineSegments[i + 1];

                if (_dotted)
                    Handles.DrawDottedLine(a, b, _gapsSize);
                else
                    Handles.DrawLine(a, b, _thickness);
            }
        }
#endif
    }

}
