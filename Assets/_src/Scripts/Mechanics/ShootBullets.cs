using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
namespace PedroAurelio
{
    public class ShootBullets : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform dynamic;

        [Header("Patterns")]
        [SerializeField] private bool isAiming;
        [SerializeField] private float initialRotation;
        [SerializeField] private int sideCount;
        [SerializeField, Range(0f, 360f)] private float angleOpening;
        [SerializeField, Range(0f, 1f)] private float missRate;
        [SerializeField, Range(0f, 360f)] private float missAngleOpening;

        [Header("Settings")]
        [SerializeField] private bool needInput;
        [SerializeField] private float startDelay;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float fireRate;
        [SerializeField] private float spinRate;

        private bool _shoot;
        private float _fireTime;
        private Vector3 _rotation;

        private void Awake()
        {
            if (spawnPosition == null)
                spawnPosition = transform;

            _fireTime = startDelay;

            if (!isAiming)
                _rotation.z = initialRotation;
        }

        private void FixedUpdate()
        {
            if (_fireTime > 0f)
            {
                _fireTime -= Time.deltaTime;
                return;
            }

            if (needInput)
            {
                if (_shoot) Shoot();
                return;
            }
            
            Shoot();
        }

        private void Shoot()
        {
            UpdateRotation();

            var missRange = new Vector3(0f, 0f, missAngleOpening * Random.Range(-missRate, missRate));
            var currentRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            var direction = isAiming ? _rotation + missRange + currentRotation : _rotation + missRange;

            if (sideCount == 1)
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.Initialize(spawnPosition.position, direction, bulletSpeed);
            }
            else
            {
                var angleOffset = Vector3.zero;
                float angleDivision;

                if (angleOpening != 360f)   angleDivision = angleOpening / (sideCount - 1);
                else                        angleDivision = angleOpening / sideCount;

                for (int i = 0; i < sideCount; i++)
                {
                    angleOffset.z = angleDivision * i;

                    var bullet = Instantiate(bulletPrefab);
                    bullet.Initialize(spawnPosition.position, direction + angleOffset, bulletSpeed);
                }
            }

            _fireTime = fireRate;
        }

        private void UpdateRotation()
        {
            if (isAiming)   _rotation.z = -angleOpening * 0.5f;
            else            _rotation.z += spinRate;
        }

        public void SetShootBool(bool value) => _shoot = value;
        public void SetShootBool(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Performed: SetShootBool(true); break;
                case InputActionPhase.Canceled: SetShootBool(false); break;
                default: break;
            }
        }
    }
}