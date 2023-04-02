using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Clicks
{
    public sealed class ClickRegistrator : MonoBehaviour
    {
        private InputAction _inputAction;
        private System.Random _rng;
        private bool _clickRegistered;


        private void Start()
        {
            _rng = new();
            GenerateNewBind();
        }

        private void Update()
        {
            if (_clickRegistered)
            {
                _clickRegistered = false;
                GenerateNewBind();
            }
        }

        private void GenerateNewBind()
        {
            if (_inputAction != null)
                _inputAction.Dispose();

            char letter = GetRandomLetter();
            _inputAction = new InputAction(string.Empty,
                                           InputActionType.Button,
                                           $"<Keyboard>/{letter}",
                                           string.Empty,
                                           string.Empty,
                                           string.Empty);

            _inputAction.performed += RegisterClick;
            _inputAction.Enable();
            Debug.Log(letter);
        }

        private void RegisterClick(InputAction.CallbackContext _)
        {
            Debug.Log("click registered!");
            _clickRegistered = true;
        }

        private char GetRandomLetter()
        {
            return (char)RandomRange((int)'a', (int)'z' + 1);
        }

        private int RandomRange(int min, int max)
        {
            //return UnityEngine.Random.Range(min, max);
            return _rng.Next(min, max);
        }
    }
}
