using Game.Earners;
using UnityEngine;

namespace Game.Ui
{
    public sealed class EarnerUiInitor : MonoBehaviour
    {
        [SerializeField] private GameObject _earnerRaw;
        [SerializeField] private EarnerUi _earnerUi;


        private void Start()
        {
            _earnerUi.Init(_earnerRaw.GetComponent<IEarner>());
        }
    }
}
