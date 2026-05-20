using UnityEngine;
public class DangerZone : MonoBehaviour
{
  [SerializeField] private int damage = 25;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      GameManager.Instance.TakeDamage(damage);
      Debug.Log($"Danger! -{damage} HP");
    }
  }
}
