using System;
using _Project.Scripts.GameContext;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input
{
   
    public class PlayerTapInput : IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener 
    {
        private PlayerInputActions _actions;

        public Vector2 ScreenPosition;
        
        public event Action TouchStarted;
        public event Action TouchCanceled;

        public void Construct()
        {
            _actions = new PlayerInputActions();
            _actions.Player.Touch.started += OnTouchStarted;
            _actions.Player.Touch.canceled += OnTouchCanceled;
            _actions.Player.TouchPosition.performed += OnTouchPositionPerformed;
        }
        
        public void OnStartGame()
        {
            _actions.Enable();
        }

        public void OnPauseGame()
        {
            _actions.Disable();
        }

        public void OnResumeGame()
        {
            _actions.Enable();
        }

        public void OnFinishGame()
        {
            _actions.Disable();
        }

        private void OnTouchStarted(InputAction.CallbackContext context)
        {
            TouchStarted?.Invoke();
        }

        private void OnTouchCanceled(InputAction.CallbackContext context)
        {
            TouchCanceled?.Invoke();
        }

        private void OnTouchPositionPerformed(InputAction.CallbackContext context)
        {
            ScreenPosition = context.ReadValue<Vector2>();
        }


    }
}