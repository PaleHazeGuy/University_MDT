using UnityEngine;
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 6f;
  [SerializeField] private Transform holdPoint;
  private SortBox heldBox = null;
  private string currentZone = "";
  private SortBox nearbyBox = null;
  private void Update()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime,
   Space.World);
    if (Input.GetKeyDown(KeyCode.E))
    {
      if (heldBox == null)
        TryPickup();
      else
        TryDrop();
    }
  }
  private void TryPickup()
  {
    if (nearbyBox == null || nearbyBox.isHeld) return;
    heldBox = nearbyBox;
    heldBox.isHeld = true;
    heldBox.transform.parent = holdPoint;
    heldBox.transform.localPosition = Vector3.zero;
    heldBox.GetComponent<Collider>().enabled = false;
    Debug.Log($"หยิบกล่องล่ {heldBox.boxColor}");
  }
  private void TryDrop()
  {
    if (string.IsNullOrEmpty(currentZone)) return;
    string expectedTag = "Zone" + heldBox.boxColor;
    bool correct = currentZone == expectedTag;
    if (correct)
    {
      WarehouseManager.Instance.BoxSorted(true);
      Debug.Log($"ถูก! {heldBox.boxColor} → {currentZone}");
    }
    else
    {
      WarehouseManager.Instance.BoxSorted(false);
      Debug.Log($"ผิด! {heldBox.boxColor} → {currentZone}");
    }
    Destroy(heldBox.gameObject);
    heldBox = null;
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Box"))
      nearbyBox = other.GetComponent<SortBox>();
    if (other.tag.StartsWith("Zone"))
      currentZone = other.tag;
    // Prompt
    string prompt = "";
    if (heldBox == null && other.CompareTag("Box"))
      prompt = "กด [E] หยิบกล่องล่ ";
    else if (heldBox != null && other.tag.StartsWith("Zone"))
      prompt = $"กด [E] วางกล่องล่ {heldBox.boxColor}";
    WarehouseManager.Instance?.ShowPrompt(prompt);
  }
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Box") && nearbyBox == other.GetComponent<SortBox>())
      nearbyBox = null;
    if (other.tag.StartsWith("Zone"))
      currentZone = "";
    WarehouseManager.Instance?.ShowPrompt("");
  }
}
