using UnityEngine;
public class SortBox : MonoBehaviour
{
  [SerializeField] private float beltSpeed = 2f;
  [HideInInspector] public string boxColor = "";
  [HideInInspector] public bool isHeld = false;
  private void Update()
  {
    if (!isHeld)
      transform.Translate(Vector3.forward * beltSpeed * Time.deltaTime);
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("DropZone") && !isHeld)
    {
      Debug.Log($"กล่องล่ {boxColor} ตกปลายสาย!");
      WarehouseManager.Instance.BoxMissed();
      Destroy(gameObject);
    }
  }
}
