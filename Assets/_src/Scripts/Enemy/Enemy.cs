using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        private Animator _animator;

        protected Movement _Movement;
        protected Aim _Aim;
        protected ShootBullets _Shoot;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            TryGetComponent<Movement>(out _Movement);
            TryGetComponent<Aim>(out _Aim);
            _Shoot = GetComponentInChildren<ShootBullets>();
        }

        protected virtual void Update()
        {
            Debug.Log(_Movement.IsMoving);
            _animator.SetBool("IsMoving", _Movement.IsMoving);
        }
    }
}