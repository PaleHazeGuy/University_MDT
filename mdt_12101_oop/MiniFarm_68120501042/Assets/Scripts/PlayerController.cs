using UnityEngine;
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  private string currentZone = "";
  private GameObject currentPlot = null;
  private void Update()
  {
    // Movement
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime,
   Space.World);
    // Interaction
    if (Input.GetKeyDown(KeyCode.E))
    {
      switch (currentZone)
      {
        case "Plot":
          FarmManager.Instance.InteractPlot(currentPlot);
          break;
        case "Well": FarmManager.Instance.GetWater(); break;
        case "Market": FarmManager.Instance.SellCrops(); break;
        case "SeedShop": FarmManager.Instance.BuySeeds(); break;
      }
    }
  }
  private void OnTriggerEnter(Collider other)
  {
    currentZone = other.tag;
    if (other.CompareTag("Plot")) currentPlot = other.gameObject;
    FarmManager.Instance?.ShowPrompt(currentZone switch
    {
      "Plot" => "กด [E] โต้ตอบแป ต้ ลง",
      "Well" => "กด [E] ตักน้ำ ",
      "Market" => "กด [E] ขายผลผลิต",
      "SeedShop" => "กด [E] ซื้อเมล็ดล็ (10 เหรียญ)",
      _ => ""
    });
  }
  private void OnTriggerExit(Collider other)
  {
    currentZone = ""; currentPlot = null;
    FarmManager.Instance?.ShowPrompt("");
  }
}
