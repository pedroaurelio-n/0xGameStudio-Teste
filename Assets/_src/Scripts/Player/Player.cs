using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        private Animator _animator;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }

        private void Update()
        {
            _animator.SetBool("IsMoving", _movement.IsMoving);
        }
    }
}