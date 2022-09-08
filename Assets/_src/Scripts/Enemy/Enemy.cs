using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        protected Movement _Movement;
        protected Aim _Aim;
        protected ShootBullets _Shoot;

        private void Awake()
        {
            TryGetComponent<Movement>(out _Movement);
            TryGetComponent<Aim>(out _Aim);
            _Shoot = GetComponentInChildren<ShootBullets>();
        }
    }
}