using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.Utils;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform weaponTransform;
        [SerializeField] private Transform shootPos;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _sprite = weaponTransform.GetComponentInChildren<SpriteRenderer>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }

        private void Update()
        {
            _animator.SetBool("IsMoving", _movement.IsMoving);

            var shouldFlip = Mathf.Sign(_aim.LookDirection.x) != Mathf.Sign(_animator.transform.localScale.x);

            if (shouldFlip)
            {
                _animator.transform.localScale = VectorUtils.InvertVectorX(_animator.transform.lossyScale);
                weaponTransform.localScale = VectorUtils.InvertVectorX(weaponTransform.localScale);
                shootPos.localPosition = VectorUtils.InvertVectorX(shootPos.localPosition);

                _sprite.flipX = !_sprite.flipX;
                _sprite.flipY = !_sprite.flipY;
                _aim.SnapToAngle();
            }
        }
    }
}