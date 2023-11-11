using System.Collections.Generic;
using _Project.Scripts.World.SFX;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIInteraction : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup uiGroup;
        [SerializeField] private GraphicRaycaster raycaster;
        [SerializeField] private float selectableClearTimer;
        
        private SfxController _sfx;
        private PlayerInputActions _input;
        
        private PointerEventData _clickData;
        private List<RaycastResult> _results;
        private Selectable _lastSelectable;
        private float _lasClickTime;
        public void Construct(SfxController sfx)
        {
            _sfx = sfx;
            _clickData = new PointerEventData(EventSystem.current);
            _results = new List<RaycastResult>();
            _input = new PlayerInputActions();
            _input.Enable();
            _input.UI.Click.performed += OnClicked;
        }

        private void OnClicked(InputAction.CallbackContext obj)
        {
            var currentTime = Time.time;

            if (currentTime - _lasClickTime > selectableClearTimer) _lastSelectable = null;
            
            _lasClickTime = currentTime;            
            if (IsSelectableTouched())
            {
                _sfx.PlaySfx(SfxType.Click).SetGroup(uiGroup);
            }
        }

        private bool IsSelectableTouched()
        {
            _clickData.position = _input.UI.Point.ReadValue<Vector2>();
            _results.Clear();
            
            raycaster.Raycast(_clickData, _results);

            foreach (var result in _results)
            {
                if (result.gameObject.TryGetComponent(out Selectable selectable) && selectable != _lastSelectable)
                {
                    _lastSelectable = selectable;
                    return true;
                }
            }

            return false;
        }
    }
}