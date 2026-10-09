using UnityEngine;

namespace MDT21102.SectorDefense.Labs
{
    public class TurretFire : MonoBehaviour
    {
        [SerializeField] Transform muzzle;
        [SerializeField] Transform projectileRoot;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] float fireInterval = 0.35f;
        float shotTimer;

        void OnEnable() { shotTimer = 0f; }

        void FixedUpdate()
        {
            if (muzzle == null || projectilePrefab == null
                || projectileRoot == null) return;
            shotTimer -= Time.fixedDeltaTime;
            if (shotTimer > 0) return;
            shotTimer += fireInterval;
            GameObject shot = Instantiate(projectilePrefab,
                muzzle.position, muzzle.rotation, projectileRoot);
            shot.GetComponent<Projectile>().Initialize(muzzle.up);
        }
    }
}
