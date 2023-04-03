using System.Collections;
using UnityEngine;

namespace Game.Utils.Fadables
{
    public abstract class Fadable : MonoBehaviour
    {
        public void Fade(float time)
        {
            if(time > 0)
            {

                var fadingEnumerator = GetFadingEnumerator(time);
                StartCoroutine(fadingEnumerator);
                return;
            }

            Destroy(gameObject);
        }


        private IEnumerator GetFadingEnumerator(float time)
        {
            for (float timePassed = 0; timePassed < time; timePassed += Time.deltaTime)
            {
                SetAlpha(1f - timePassed / time);
                yield return null;
            }

            Destroy(gameObject);
        }


        protected abstract void SetAlpha(float value);
    }
}
