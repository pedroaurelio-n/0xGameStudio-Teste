using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
namespace PedroAurelio
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _fillImage;

        private Transform _target;
        private Vector3 _offset;

        public void Initialize(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
            HealthBarManager.AttachBarToCanvas(this);
        }

        public void UpdatePosition()
        {
            if (_target != null)
                transform.position = _target.position + _offset;
        }

        public void UpdateHealth(float currentHealth, float maxHealth) => _fillImage.fillAmount = currentHealth / maxHealth;
    }
}