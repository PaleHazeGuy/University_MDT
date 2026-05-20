using UnityEngine;
public class LaunchPad : MonoBehaviour
{
  [SerializeField] private float launchForce = 12f;
  [SerializeField] private Vector3 launchDirection = Vector3.up;
  private void OnCollisionEnter(Collision collision)
  {
    Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
    if (otherRb != null)

      otherRb.velocity = Vector3.zero;
    otherRb.AddForce(launchDirection.normalized * launchForce,
   ForceMode.Impulse);
    Debug.Log($"LAUNCH! {collision.gameObject.name}");
  }

}
