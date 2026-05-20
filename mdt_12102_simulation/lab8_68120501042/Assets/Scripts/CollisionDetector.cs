using UnityEngine;
public class CollisionDetector : MonoBehaviour
{
  [SerializeField] private Color hitColor = Color.yellow;
  private Color originalColor;
  private Renderer objectRenderer;
  private void Start()
  {
    objectRenderer = GetComponent<Renderer>();
    if (objectRenderer != null) originalColor = objectRenderer.material.color;
  }
  private void OnCollisionEnter(Collision collision)
  {
    Debug.Log($"[COLLISION] {name} ชน {collision.gameObject.name}");
    Debug.Log($" Force: {collision.relativeVelocity.magnitude:F2}");
    if (objectRenderer != null) objectRenderer.material.color = hitColor;
  }
  private void OnCollisionExit(Collision collision)
  {
    Debug.Log($"[EXIT] {name} ออกจาก {collision.gameObject.name}");
    if (objectRenderer != null) objectRenderer.material.color = originalColor;
  }
}
