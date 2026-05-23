using UnityEngine;
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private GameObject heldItemVisual;
  private string currentZone = "";
  private GameObject currentTarget = null;
  [HideInInspector] public string heldIngredient = "";
  [HideInInspector] public string cookedDish = "";
  private void Update()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime,
   Space.World);
    if (Input.GetKeyDown(KeyCode.E))
    {
      switch (currentZone)
      {
        case "Ingredient":
          PickIngredient(); break;
        case "Cooker":
          ShopManager.Instance.StartCooking(this); break;
        case "Counter":
          ShopManager.Instance.ServeCustomer(this); break;
      }
    }
  }
  private void PickIngredient()
  {
    if (currentTarget == null) return;
    Renderer r = currentTarget.GetComponent<Renderer>();
    heldIngredient = currentTarget.name;
    cookedDish = "";
    if (heldItemVisual != null)
    {
      heldItemVisual.SetActive(true);
      heldItemVisual.GetComponent<Renderer>().material.color
      = r.material.color;
    }
    Debug.Log($"หยิบ: {heldIngredient}");
  }
  public void SetCookedDish(string dish, Color c)
  {
    cookedDish = dish;
    heldIngredient = "";
    if (heldItemVisual != null)
    {
      heldItemVisual.SetActive(true);
      heldItemVisual.GetComponent<Renderer>().material.color = c;
    }
  }
  public void ClearHeld()
  {
    heldIngredient = ""; cookedDish = "";
    if (heldItemVisual != null) heldItemVisual.SetActive(false);
  }
  private void OnTriggerEnter(Collider other)
  {
    currentZone = other.tag;
    currentTarget = other.gameObject;
    ShopManager.Instance?.ShowPrompt(currentZone switch
    {
      "Ingredient" => $"กด [E] หยิบ {other.name}",
      "Cooker" => "กด [E] ทำ อาหาร",
      "Counter" => "กด [E] เสิร์สิร์ฟลูกค้าค้",
      _ => ""
    });
  }
  private void OnTriggerExit(Collider other)
  {
    currentZone = ""; currentTarget = null;
    ShopManager.Instance?.ShowPrompt("");
  }
}
