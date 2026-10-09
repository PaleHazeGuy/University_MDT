using UnityEngine;
using UnityEngine.InputSystem;

namespace MDT21102.SectorDefense.Lab1
{
    public class KeyboardAimInput : MonoBehaviour
    {
        [SerializeField] AimInput input;
        [SerializeField] InputActionReference aimAction;
        [SerializeField] float changeSpeed = 1.5f;

        void OnEnable()
        {
            if (aimAction != null)
                aimAction.action.Enable();
        }

        void OnDisable()
        {
            if (aimAction != null)
                aimAction.action.Disable();
        }

        void Update()
        {
            if (input == null || aimAction == null) return;

            float horizontal = aimAction.action.ReadValue<float>();
            float nextAim = input.NormalizedAim
                + horizontal * changeSpeed * Time.deltaTime;

            input.SetNormalizedAim(nextAim);
        }
    }
}
