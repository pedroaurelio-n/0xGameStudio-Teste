using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.Utils;
 
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
            _animator.SetBool("IsMoving", _Movement.IsMoving);

            // Checagem para apontar o sprite na direção da mira. 
            // Player.cs tem o mesmo código, mas em vez de == é usado !=, que seria a condição correta
            // Como os sprites dos inimigos por padrão foram desenhados apontando para a esquerda (os do player são para a direita) essa mudança foi necessária
            var shouldFlip = Mathf.Sign(_Aim.LookDirection.x) == Mathf.Sign(_animator.transform.localScale.x);
            
            if (shouldFlip)
                _animator.transform.localScale = VectorUtils.InvertVectorX(_animator.transform.localScale);                
        }
    }
}