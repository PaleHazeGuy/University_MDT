using UnityEngine;

namespace MDT21102.SectorDefense.Lab1
{
    public class TurretAim : MonoBehaviour
    {
        [SerializeField] AimInput input;
        [SerializeField] Transform turretPivot;
        [SerializeField] float maxAimAngle = 75f;
        [SerializeField] float turnSpeed = 120f;
        [SerializeField] float actualAngle;

        public void Tick(float dt)
        {
            if (input == null || turretPivot == null) return;

            float targetAngle = input.NormalizedAim * maxAimAngle;
            actualAngle = Mathf.MoveTowards(
                actualAngle, targetAngle, turnSpeed * dt);
            turretPivot.localRotation = Quaternion.Euler(0f, 0f, -actualAngle);
        }

        void Update()
        {
            Tick(Time.deltaTime);
        }
    }
}
