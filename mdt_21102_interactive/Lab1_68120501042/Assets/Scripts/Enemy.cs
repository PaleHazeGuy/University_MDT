using UnityEngine;

namespace MDT21102.SectorDefense.Labs
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform baseTarget;
        [SerializeField] int maxHealth = 1;
        [SerializeField] float moveSpeed = 2.4f;
        [SerializeField] float baseHitRadius = 0.5f;
        [SerializeField] int health;
        [SerializeField] bool resolved;
        Rigidbody2D body;

        public int Health => health;
        public bool Resolved => resolved;
        public event System.Action Damaged;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            health = maxHealth;
        }

        void FixedUpdate()
        {
            if (resolved || baseTarget == null) return;
            Vector2 targetPosition = baseTarget.position;
            if (Vector2.Distance(body.position, targetPosition)
                <= baseHitRadius)
            {
                Resolve();
                return;
            }
            Vector2 nextPosition = Vector2.MoveTowards(body.position,
                targetPosition, moveSpeed * Time.fixedDeltaTime);
            body.MovePosition(nextPosition);
        }

        public void ApplyDamage(int amount)
        {
            if (resolved || amount <= 0) return;
            health = Mathf.Max(0, health - amount);
            Damaged?.Invoke();
            if (health == 0) Resolve();
        }

        void Resolve()
        {
            if (resolved) return;
            resolved = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
