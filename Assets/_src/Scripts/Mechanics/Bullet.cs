using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
 
namespace PedroAurelio
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject particleObject;

        [Header("Settings")]
        [SerializeField] private float damage;

        private Rigidbody2D _rigidbody;
        private ParticleSystem _particleSystem;

        private IObjectPool<Bullet> _pool;
        private bool _isActiveOnPool;
        private WaitForSeconds _waitForParticles;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _particleSystem = particleObject.GetComponent<ParticleSystem>();

            _waitForParticles = new WaitForSeconds(_particleSystem.main.duration);
        }

        public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        public void Initialize(Vector3 position, Vector3 rotation, float speed)
        {
            particleObject.transform.SetParent(transform);
            _isActiveOnPool = true;

            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Health>(out Health target))
                target.HealthValue -= damage;
                
            if (_isActiveOnPool)
                StartCoroutine(ReleaseFromPoolCoroutine());
        }

        private IEnumerator ReleaseFromPoolCoroutine()
        {
            particleObject.transform.SetParent(transform.parent);
            _particleSystem.Play();
            gameObject.SetActive(false);

            yield return _waitForParticles;
            
            _pool.Release(this);
            _isActiveOnPool = false;
        }
    }
}