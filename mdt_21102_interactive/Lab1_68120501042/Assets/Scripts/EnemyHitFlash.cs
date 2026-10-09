using UnityEngine;

namespace MDT21102.SectorDefense.Labs
{
    public class EnemyHitFlash : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        [SerializeField] SpriteRenderer target;
        [SerializeField] Color flashColor =
            new Color(1f, 0.25f, 0.25f, 1f);
        [SerializeField] float flashDuration = 0.15f;
        Color normalColor;
        float remaining;

        void Awake()
        {
            if (target != null) normalColor = target.color;
        }

        void Restore()
        {
            remaining = 0;
            if (target != null) target.color = normalColor;
        }

        void Flash()
        {
            if (target == null) return;
            remaining = flashDuration;
            target.color = flashColor;
        }

        void Update()
        {
            if (remaining <= 0) return;
            remaining -= Time.deltaTime;
            if (remaining <= 0) Restore();
        }

        void OnEnable()
        {
            if (enemy != null) enemy.Damaged += Flash;
        }

        void OnDisable()
        {
            if (enemy != null) enemy.Damaged -= Flash;
            Restore();
        }
    }
}
