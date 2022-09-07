using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Movement))]
    public class PlayerInput : MonoBehaviour
    {
        private Movement _movement;
        private PlayerControls _controls;

        private Camera _mainCamera;

        private void Awake()
        {
            _movement = GetComponent<Movement>();

            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new PlayerControls();

                _controls.Gameplay.Move.performed += _movement.SetCurrentDirection;

                _controls.Enable();
            }
        }

        private void OnDisable() => _controls.Disable();
    }
}