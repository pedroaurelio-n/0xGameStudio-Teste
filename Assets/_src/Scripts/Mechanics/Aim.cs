using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    public class Aim : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform rotatingObject;

        [Header("Settings")]
        [SerializeField, Range(100f, 1500f)] private float rotationSpeed = 500f;

        private Transform _targetReference;

        private Vector2 _aimPosition;
        private Vector2 _rawDirection;
        private Vector2 _lookDirection;

        private void Awake()
        {
            if (rotatingObject == null)
                rotatingObject = transform;
        }

        private void Update()
        {
            rotatingObject.rotation = Quaternion.Euler(0f, 0f, GetRotationAngle());
            _lookDirection = rotatingObject.right;
        }

        private float GetRotationAngle()
        {
            _rawDirection = _aimPosition - (Vector2)rotatingObject.position;

            var desiredAngle = Mathf.Atan2(_rawDirection.y, _rawDirection.x) * Mathf.Rad2Deg;

            return Mathf.MoveTowardsAngle(rotatingObject.eulerAngles.z, desiredAngle, rotationSpeed * Time.deltaTime);
        }

        public void SetAimDirection(Vector2 position) => _aimPosition = position;
        public void SetAimDirection(Transform target)
        {
            _targetReference = target;
            SetAimDirection(_targetReference.position);
        }
    }
}