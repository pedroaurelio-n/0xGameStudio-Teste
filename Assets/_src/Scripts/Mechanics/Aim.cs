using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    public class Aim : MonoBehaviour
    {
        public Vector2 LookDirection { get; private set; }
        
        [Header("Dependencies")]
        [SerializeField] private Transform aimObject;

        [Header("Settings")]
        [SerializeField] private bool startFacingDirection;
        [SerializeField, Range(100f, 1500f)] private float rotationSpeed = 500f;

        private Transform _targetReference;

        private Vector2 _aimPosition;
        private Vector2 _rawDirection;

        private void Awake()
        {
            if (aimObject == null)
                aimObject = transform;
        }

        private void Start()
        {
            if (startFacingDirection)
            {
                var desiredAngle = GetDesiredAngle();
                aimObject.rotation = Quaternion.Euler(0f, 0f, desiredAngle);
            }
        }

        private void Update()
        {
            aimObject.rotation = Quaternion.Euler(0f, 0f, RotateTowardsAngle());
            LookDirection = aimObject.right;
        }

        private float RotateTowardsAngle()
        {
            var desiredAngle = GetDesiredAngle();
            return Mathf.MoveTowardsAngle(aimObject.eulerAngles.z, desiredAngle, rotationSpeed * Time.deltaTime);
        }

        private float GetDesiredAngle()
        {
            _rawDirection = _aimPosition - (Vector2)aimObject.position;
            return Mathf.Atan2(_rawDirection.y, _rawDirection.x) * Mathf.Rad2Deg;
        }

        public void SetAimDirection(Vector2 position) => _aimPosition = position;
        public void SetAimDirection(Transform target)
        {
            _targetReference = target;
            SetAimDirection(_targetReference.position);
        }
    }
}