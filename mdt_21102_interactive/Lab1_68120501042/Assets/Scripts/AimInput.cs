using UnityEngine;

namespace MDT21102.SectorDefense.Lab1
{
    public class AimInput : MonoBehaviour
    {
        [SerializeField, Range(-1f, 1f)] float normalizedAim;
        public float NormalizedAim => normalizedAim;

        public void SetNormalizedAim(float value)
        {
            normalizedAim = Mathf.Clamp(value, -1f, 1f);
        }
    }
}
