using UnityEngine;
public class BallController : MonoBehaviour
{
  [SerializeField] private float moveForce = 10f;
  [SerializeField] private float maxSpeed = 8f;
  public Rigidbody rb;
  private int score = 0;
  public Vector3 spawnPoint;
  private void Start() { spawnPoint = transform.position; rb = GetComponent<Rigidbody>(); }
  private void FixedUpdate()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    rb.AddForce(new Vector3(h, 0, v) * moveForce);
    Vector3 vel = rb.velocity;
    if (vel.magnitude > maxSpeed)
      rb.velocity = vel.normalized * maxSpeed;
  }

  public void AddScore(int val)
  {
    score += val;
    Debug.Log($"Current Score: {score}");
  }
}
