using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector3 position, Vector3 rotation, float speed)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Health>(out Health target))
                target.HealthValue -= damage;
                
            Destroy(gameObject);            
        }
    }
}