using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
    public abstract class SoDatabaseElement : ScriptableObject
    {
        [SerializeField, HideInInspector] private int _soDatabaseID;


        public int GetDatabaseId() => _soDatabaseID;

        internal void SetDatabaseId(int id) => _soDatabaseID = id;
    }

    public abstract class SoDatabase<TElement> : ScriptableObject
        where TElement : SoDatabaseElement
    {
        [SerializeField] private List<TElement> _newElements;

        [SerializeField] private List<TElement> _registeredElements;

#if UNITY_EDITOR
        private HashSet<TElement> _hs;
#endif


#if UNITY_EDITOR
        private HashSet<TElement> HS
        {
            get
            {
                if (_hs == null)
                    _hs = new();
                else
                    _hs.Clear();

                return _hs;

            }
        }
#endif


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_newElements.Count > 0)
            {
                _registeredElements.AddRange(_newElements);
                _newElements.Clear();
                UnityEditor.EditorUtility.SetDirty(this);
            }

            CheckRegisteredElements();
        }

        private void CheckRegisteredElements()
        {
            var list = _registeredElements;
            var count = list.Count;

            bool modified = EnsureElementsUniqueness(in list, in count);

            if (modified)
                count = list.Count;

            _ = ReAssignIds(in list, 0, in count);
        }

        private bool ReAssignIds(in List<TElement> list, int start, in int length)
        {
            bool reassigned = false;

            for (int i = start; i < start + length; i++)
            {
                var el = list[i];

                if (el.GetDatabaseId() == i)
                    continue;

                reassigned = true;
                el.SetDatabaseId(i);
                UnityEditor.EditorUtility.SetDirty(el);
            }

            return reassigned;
        }

        private bool EnsureElementsUniqueness(in List<TElement> list, in int count)
        {
            var hs = HS;

            for (int i = 0; i < count; i++)
            {
                if (list[i] != null)
                    _ = hs.Add(list[i]);
            }

            bool modified = hs.Count != count;

            if (modified)
            {
                list.Clear();
                list.AddRange(hs);
                UnityEditor.EditorUtility.SetDirty(this);
            }

            return modified;
        }
#endif
        public TElement GetElement(int id)
        {
            return _registeredElements[id];
        }
    }
}
