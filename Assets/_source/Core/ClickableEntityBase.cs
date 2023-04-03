using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Clicks
{
    public sealed class ClickableEntity : MonoBehaviour
    {
        [System.Serializable]
        private sealed class State
        {
            [SerializeField] private GameObject _vfxPrefab;
            [SerializeField] private GameObject _visual;


            public void Enter()
            {

            }
        }


        [SerializeField] private State[] _states;


        public event System.Action<ClickableEntity> Finished;



    }
    //public delegate void ClickableFinished(ClickableEntityBase clickable);

    //public delegate void ClickableProgressChanged(ClickableEntityBase clickable, float progress, float delta);


    //public abstract class ClickableEntityBase : MonoBehaviour, IPointerDownHandler
    //{
    //    private float _progress;


    //    public float Progress => _progress;


    //    public event ClickableProgressChanged ProgressChanged;
    //    public event ClickableFinished Finished;


    //    protected void ChangeProgress(float delta)
    //    {
    //        _progress += delta;

    //        bool finished = false;

    //        if (_progress >= 1f)
    //        {
    //            _progress = 1f;
    //            finished = true;
    //        }

    //        ProgressChanged?.Invoke(this, _progress, delta);

    //        if (finished)
    //            Finished?.Invoke(this);
    //    }


    //    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    //    {
    //        OnHit();
    //    }


    //    protected abstract void OnHit();
    //}
}
