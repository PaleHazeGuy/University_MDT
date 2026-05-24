using UnityEngine;
using TMPro;
public class WarehouseManager : MonoBehaviour
{
  public static WarehouseManager Instance { get; private set; }
  [Header("Spawning")]
  [SerializeField] private GameObject[] boxPrefabs;
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private float spawnInterval = 4f;
  [SerializeField] private float spawnXRange = 0.5f;
  [Header("Difficulty")]
  [SerializeField] private float minInterval = 1.5f;
  [SerializeField] private float speedUpEvery = 10f;
  [SerializeField] private float speedUpAmount = 0.3f;
  [Header("Economy")]
  [SerializeField] private int score = 0;
  [SerializeField] private int correctPoints = 10;
  [SerializeField] private int wrongPenalty = 5;
  [SerializeField] private int missedPenalty = 5;
  [Header("Game Timer")]
  [SerializeField] private float gameTime = 90f;
  [Header("UI")]
  [SerializeField] private TMP_Text scoreText;
  [SerializeField] private TMP_Text timerText;
  [SerializeField] private TMP_Text promptText;
  [SerializeField] private TMP_Text comboText;
  private float spawnTimer = 0f;
  private float difficultyTimer = 0f;
  private int sorted = 0;
  private int missed = 0;
  private int combo = 0;
  private bool gameOver = false;
  private void Awake()
  {
    if (Instance != null) { Destroy(gameObject); return; }
    Instance = this;
  }
  private void Start() { UpdateUI(); }
  private void Update()
  {
    if (gameOver) return;
    // Game Timer
    gameTime -= Time.deltaTime;
    if (timerText != null)
      timerText.text = $"Time: {Mathf.CeilToInt(gameTime)}";
    if (gameTime <= 0)
    { GameOver(); return; }
    // Spawn Timer
    spawnTimer += Time.deltaTime;
    if (spawnTimer >= spawnInterval)
    { SpawnBox(); spawnTimer = 0f; }
    // Difficulty ramp
    difficultyTimer += Time.deltaTime;
    if (difficultyTimer >= speedUpEvery)
    {
      spawnInterval = Mathf.Max(minInterval,
      spawnInterval - speedUpAmount);
      difficultyTimer = 0f;
      Debug.Log($"เร็วขึ้น! Interval: {spawnInterval:F1}s");
    }
  }
  private void SpawnBox()
  {
    int idx = Random.Range(0, boxPrefabs.Length);
    float xPos = spawnPoint.position.x
    + Random.Range(-spawnXRange, spawnXRange);
    Vector3 pos = new Vector3(xPos,
    spawnPoint.position.y, spawnPoint.position.z);
    Instantiate(boxPrefabs[idx], pos, Quaternion.identity);
  }
  public void BoxSorted(bool correct)
  {
    if (correct)
    {
      combo++;
      int bonus = combo >= 3 ? correctPoints * 2 : correctPoints;
      score += bonus;
      sorted++;
      Debug.Log($"ถูก! +{bonus}" +
      (combo >= 3 ? $" COMBO x{combo}!" : ""));
    }
    else
    {
      score -= wrongPenalty;
      combo = 0;
      Debug.Log($"ผิดโซน! -{wrongPenalty}");
    }
    UpdateUI();
  }
  public void BoxMissed()
  {
    score -= missedPenalty;
    missed++;
    combo = 0;
    Debug.Log($"ตกสาย! -{missedPenalty}");
    UpdateUI();
  }
  private void GameOver()
  {
    gameOver = true;
    Debug.Log($"GAME OVER! Score:{score} Sorted:{sorted} Missed:{missed}");
    if (timerText != null) timerText.text = "TIME UP!";
    // TODO: แสดง GameOver Panel
  }
  public void ShowPrompt(string t)
  { if (promptText != null) promptText.text = t; }
  private void UpdateUI()
  {
    if (scoreText != null)
      scoreText.text = $"คะแนน: {score} ✅ {sorted} ❌ {missed}";
    if (comboText != null)
      comboText.text = combo >= 2 ? $"COMBO x{combo}!" : "";
  }
}
