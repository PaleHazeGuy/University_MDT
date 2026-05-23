using UnityEngine;
using TMPro;
public class ShopManager : MonoBehaviour
{
  public static ShopManager Instance { get; private set; }
  [Header("Spawning")]
  [SerializeField] private GameObject customerPrefab;
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private Transform waitPoint;
  [SerializeField] private float spawnInterval = 8f;
  [Header("Cooking")]
  [SerializeField] private float cookDuration = 3f;
  [SerializeField] private Renderer cookerRenderer;
  [Header("Economy")]
  [SerializeField] private int money = 0;
  [SerializeField] private int serveReward = 30;
  [SerializeField] private int missedPenalty = 10;
  [Header("UI")]
  [SerializeField] private TMP_Text statusText;
  [SerializeField] private TMP_Text promptText;
  [SerializeField] private TMP_Text timerText;
  [Header("Game Timer")]
  [SerializeField] private float gameTime = 90f;
  private CustomerNPC currentCustomer;
  private float spawnTimer = 0f;
  private bool isCooking = false;
  private float cookTimer = 0f;
  private string cookingDish = "";
  private Color cookingColor;
  private Color cookerOriginalColor;
  private int served = 0;
  private int missed = 0;
  // สีและชื่อของเมนู
  private readonly string[] orderNames = { "Red", "Green", "Yellow" };
  private readonly Color[] orderColors = { Color.red, Color.green,
Color.yellow };
  private void Awake()
  {
    if (Instance != null) { Destroy(gameObject); return; }
    Instance = this;
  }
  private void Start()
  {
    if (cookerRenderer != null)
      cookerOriginalColor = cookerRenderer.material.color;
    SpawnCustomer();
    UpdateUI();
  }
  private void Update()
  {
    // Game Timer
    gameTime -= Time.deltaTime;
    if (timerText != null)
      timerText.text = $"Time: {Mathf.CeilToInt(gameTime)}";
    if (gameTime <= 0)
    {
      Debug.Log($"TIME UP! Served:{served} Missed:{missed} Money: {money}"); enabled = false; return;
    }
    // Spawn NPC
    if (currentCustomer == null)
    {
      spawnTimer += Time.deltaTime;
      if (spawnTimer >= spawnInterval)
      { SpawnCustomer(); spawnTimer = 0f; }
    }
    // Cooking Timer
    if (isCooking)
    {
      cookTimer += Time.deltaTime;
      float p = cookTimer / cookDuration;
      if (cookerRenderer != null)
        cookerRenderer.material.color = Color.Lerp(cookerOriginalColor,
       cookingColor, p);
      if (cookTimer >= cookDuration)
      {
        isCooking = false;
        if (cookerRenderer != null)
          cookerRenderer.material.color = cookingColor;
        Debug.Log($"ทำ เสร็จ! {cookingDish}");
      }
    }
  }
  private void SpawnCustomer()
  {
    int idx = Random.Range(0, orderNames.Length);
    GameObject go = Instantiate(customerPrefab,
    spawnPoint.position, Quaternion.identity);
    currentCustomer = go.GetComponent<CustomerNPC>();
    currentCustomer.Setup(waitPoint, spawnPoint, orderNames[idx],
   orderColors[idx]);
    Debug.Log($"ลูกค้ามา ค้ ! สั่งสั่ : {orderNames[idx]}");
    UpdateUI();
  }
  public void StartCooking(PlayerController player)
  {
    if (isCooking) { Debug.Log("กำ ลังทำ อยู่!"); return; }
    if (string.IsNullOrEmpty(player.heldIngredient))
    { Debug.Log("ไม่มี ม่ วัตถุดิบ!"); return; }
    string ingr = player.heldIngredient;
    // map ingredient เป็นชื่อ dish (ใช้ชื่อเดียวกันในตัวอย่างย่ นี้)
    cookingDish = ingr.Contains("Red") ? "Red" :
    ingr.Contains("Green") ? "Green" : "Yellow";
    cookingColor = cookingDish == "Red" ? Color.red :
    cookingDish == "Green" ? Color.green : Color.yellow;
    player.ClearHeld();
    isCooking = true;
    cookTimer = 0f;
    Debug.Log($"เริ่มทำ : {cookingDish}");
  }
  public void ServeCustomer(PlayerController player)
  {
    if (currentCustomer == null) { Debug.Log("ไม่มี ม่ ลูกค้าค้"); return; }
    if (isCooking) { Debug.Log("อาหารยังทำ ไม่เม่ สร็จ!"); return; }
    if (string.IsNullOrEmpty(cookingDish))
    { Debug.Log("ยังไม่ไม่ ด้ทำด้ ทำอาหาร"); return; }
    if (currentCustomer.TryServe(cookingDish))
    {
      money += serveReward;
      served++;
      Debug.Log($"เสิร์สิร์ฟสำ เร็จ! +{serveReward} เหรียญ");
    }
    else { Debug.Log("อาหารผิด!"); }
    cookingDish = ""; player.ClearHeld();
    if (cookerRenderer != null)
      cookerRenderer.material.color = cookerOriginalColor;
    currentCustomer = null;
    UpdateUI();
  }
  public void CustomerLeft(bool wasServed)
  {
    if (!wasServed)
    { missed++; money -= missedPenalty; }
    currentCustomer = null;
    UpdateUI();
  }
  public void ShowPrompt(string t)
  { if (promptText != null) promptText.text = t; }
  private void UpdateUI()
  {
    if (statusText != null)
      statusText.text = $"�{money} ✅{served} ❌{missed}";
  }
}
