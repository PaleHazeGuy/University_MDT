using UnityEngine;
using TMPro;
public class FarmManager : MonoBehaviour
{
  public static FarmManager Instance { get; private set; }
  [Header("Resources")]
  [SerializeField] private int coins = 50;
  [SerializeField] private int seeds = 3;
  [SerializeField] private int harvested = 0;
  [SerializeField] private bool hasWater = false;
  [Header("Prices")]
  [SerializeField] private int seedPrice = 10;
  [SerializeField] private int sellPrice = 25;
  [Header("UI")]
  [SerializeField] private TMP_Text statusText;
  [SerializeField] private TMP_Text promptText;
  private void Awake()
  {
    if (Instance != null) { Destroy(gameObject); return; }
    Instance = this;
  }
  private void Start() { UpdateUI(); }
  public void InteractPlot(GameObject plotObj)
  {
    if (plotObj == null) return;
    CropPlot plot = plotObj.GetComponent<CropPlot>();
    if (plot == null) return;
    if (plot.TryHarvest())
    { harvested++; UpdateUI(); return; }
    if (hasWater && plot.TryWater())
    { hasWater = false; UpdateUI(); return; }
    if (seeds > 0 && plot.TryPlant())
    { seeds--; UpdateUI(); return; }
    Debug.Log("ทำ อะไรไม่ไม่ ด้ (เมล็ดหมด ล็ /น้ำ หมด/กำ ลังโต)");
  }
  public void GetWater()
  { hasWater = true; Debug.Log("ตักน้ำ !"); UpdateUI(); }
  public void SellCrops()
  {
    if (harvested <= 0) { Debug.Log("ไม่มี ม่ ผลผลิตขาย"); return; }
    int earned = harvested * sellPrice;
    coins += earned;
    Debug.Log($"ขาย {harvested} ชิ้น ไ ด้ {earned} เหรียญ!");
    harvested = 0; UpdateUI();
  }
  public void BuySeeds()
  {
    if (coins < seedPrice) { Debug.Log("เงินไม่พม่ อ!"); return; }
    coins -= seedPrice; seeds += 3;
    Debug.Log($"ซื้อเมล็ดล็ 3 เมล็ด เห ล็ ลือ {seeds}"); UpdateUI();
  }
  public void ShowPrompt(string text)
  { if (promptText != null) promptText.text = text; }
  private void UpdateUI()
  {
    if (statusText != null)
      statusText.text = $" {coins} {seeds} {harvested} 💰 🌱 🌾 💧 {(hasWater ? "มี" : "-")} ";
  }
}

