using UnityEngine;
public class FirstPersonController : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] private float moveSpeed = 5f;
  [Header("Mouse Look")]
  [SerializeField] private float mouseSensitivity = 2f;
  [SerializeField] private float maxLookAngle = 80f;
  private Transform cameraTransform;
  private Rigidbody rb;
  private float xRotation = 0f;
  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    cameraTransform = Camera.main.transform;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }
  private void Update()
  {
    HandleMouseLook();
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
  }
  private void FixedUpdate()
  {
    HandleMovement();
  }
  private void HandleMouseLook()
  {
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
    transform.Rotate(0, mouseX, 0);
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
    cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
  }
  private void HandleMovement()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    Vector3 move = transform.right * h + transform.forward * v;
    Vector3 newVel = move * moveSpeed;
    newVel.y = rb.velocity.y;
    rb.velocity = newVel;
  }
}
