using UnityEngine;

namespace MDT21102.SectorDefense.Labs
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 18f;
        [SerializeField] float lifetime = 1.6f;
        Rigidbody2D body;
        Vector2 direction;
        bool consumed;

        void Awake() { body = GetComponent<Rigidbody2D>(); }

        public void Initialize(Vector2 heading)
        {
            direction = heading.normalized;
        }

        void FixedUpdate()
        {
            if (consumed) return;
            body.MovePosition(body.position
                + direction * speed * Time.fixedDeltaTime);
            lifetime -= Time.fixedDeltaTime;
            if (lifetime <= 0) Remove();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (consumed) return;
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy == null || enemy.Resolved) return;
            consumed = true;
            enemy.ApplyDamage(1);
            Remove();
        }

        void Remove()
        {
            consumed = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
