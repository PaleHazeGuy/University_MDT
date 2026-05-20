using UnityEngine;
public class Collectible : MonoBehaviour
{
  [SerializeField] private float rotateSpeed = 90f;
  [SerializeField] private float bobSpeed = 2f;
  [SerializeField] private float bobHeight = 0.3f;
  private Vector3 startPos;
  private void Start() { startPos = transform.position; }
  private void Update()
  {
    transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    transform.position = new Vector3(startPos.x, newY, startPos.z);
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      BallController controller = other.GetComponent<BallController>();
      if (controller != null)
      {
        controller.AddScore(1);
      }
      Debug.Log($"เก็บก็ {name}!"); Destroy(gameObject);
    }
  }
}
