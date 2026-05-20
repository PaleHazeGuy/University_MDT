using UnityEngine;

public class DangerZone : MonoBehaviour
{
  private void Start() { }
  private void Update() { }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      BallController controller = other.GetComponent<BallController>();
      if (controller != null)
      {
        controller.rb.velocity = Vector3.zero;
        other.transform.position = controller.spawnPoint;
      }
    }
  }
}
