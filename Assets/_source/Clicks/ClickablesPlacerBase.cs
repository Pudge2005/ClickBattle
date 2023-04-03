using UnityEngine;

namespace Game.Clicks
{
    public abstract class ClickablesPlacerBase : MonoBehaviour
    {
        [SerializeField] private ClickRegistrator _clicksRegistrator;


        protected void InitClickable(ClickableTarget clickable)
        {
            clickable.Init(_clicksRegistrator);
        }
    }
}
