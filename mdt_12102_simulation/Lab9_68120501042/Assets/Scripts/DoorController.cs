using UnityEngine;
public class DoorController : MonoBehaviour
{
  [SerializeField] private float openAngle = 90f;
  [SerializeField] private float speed = 3f;
  private bool isOpen = false;
  private Quaternion closedRotation;
  private Quaternion openRotation;
  private void Start()
  {
    closedRotation = transform.rotation;
    openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
  }
  private void Update()
  {
    Quaternion target = isOpen ? openRotation : closedRotation;
    transform.rotation = Quaternion.Lerp(
    transform.rotation, target, Time.deltaTime * speed);
  }
  public void ToggleDoor()
  {
    isOpen = !isOpen; Debug.Log($"Door {name}: {(isOpen ? "OPEN" : "CLOSED")}");
  }
}
