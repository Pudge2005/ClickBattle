using UnityEngine;

namespace Game.Entities
{
    [System.Serializable]
    internal sealed class ClickableEffect
    {
        [SerializeField] private GameObject _particlesPrefab;
        [SerializeField] private AudioClip _sound;


        public void Act(Vector3 position)
        {
            if (_particlesPrefab != null)
                _ = GameObject.Instantiate(_particlesPrefab, position, Quaternion.identity);

            if (_sound != null)
                AudioSource.PlayClipAtPoint(_sound, position);
        }
    }
}
