using UnityEngine;

public class LifecycleDemo : MonoBehaviour
{
  [SerializeField] private string objectName = "MyObject";
  [SerializeField] private float rotationSpeed = 50f;
  private void Awake()
  { Debug.Log($"[{objectName}] Awake() - ก่อนก่ Start"); }
  private void Start()
  {
    Debug.Log($"[{objectName}] Start() - หลัง Awake");
    Debug.Log($"Position: {transform.position}");
    Debug.Log($"Parent: {transform.parent?.name ?? "None"}");
  }
  private void Update()
  { transform.Rotate(0, rotationSpeed * Time.deltaTime, 0); }
  private void OnDestroy()
  { Debug.Log($"[{objectName}] OnDestroy()"); }
}
