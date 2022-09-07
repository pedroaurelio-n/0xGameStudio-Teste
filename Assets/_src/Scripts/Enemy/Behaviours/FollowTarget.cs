using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    public class FollowTarget : Enemy
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            _Shoot?.SetShootBool(true);
        }

        private void Update()
        {
            _Aim.SetAimDirection(target);
            _Movement.SetCurrentDirection(_Aim.LookDirection);
        }
    }
}