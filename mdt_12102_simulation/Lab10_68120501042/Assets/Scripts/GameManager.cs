using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  [Header("UI References")]
  [SerializeField] private TMP_Text scoreText;
  [SerializeField] private Image healthFill;

  [Header("Game Settings")]
  [SerializeField] private int maxHealth = 100;

  [Header("Timer")]
  [SerializeField] private TMP_Text timerText;
  [SerializeField] private float timeLimit = 60f;

  [Header("Audio")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip collectSound;
  [SerializeField] private AudioClip damageSound;
  [SerializeField] private AudioClip gameOverSound;
  [SerializeField] private AudioClip heartbeatSound;

  [Header("Menu")]
  [SerializeField] private GameObject gameOverPanel;
  [SerializeField] private UnityEngine.UI.Button restartButton;

  private int score = 0;
  private int currentHealth;
  private float heartbeatTimer = 0f;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    { Destroy(gameObject); return; }
    Instance = this;
  }

  private void Start()
  {
    currentHealth = maxHealth;
    UpdateUI();
    if (gameOverPanel != null) gameOverPanel.SetActive(false);
    if (restartButton != null)
      restartButton.onClick.AddListener(RestartGame);
  }

  private void Update()
  {
    timeLimit -= Time.deltaTime;
    int minutes = (int)(timeLimit / 60);
    int seconds = (int)(timeLimit % 60);
    if (timerText != null)
    {
      timerText.text = $"Timer: {minutes:00}:{seconds:00}";
      timerText.color = timeLimit < 10f ? Color.red : Color.white;
    }
    if (timeLimit <= 0) GameOver();

    if ((float)currentHealth / maxHealth < 0.3f)
    {
      heartbeatTimer -= Time.deltaTime;
      if (heartbeatTimer <= 0f)
      {
        if (audioSource != null && heartbeatSound != null)
          audioSource.PlayOneShot(heartbeatSound);
        heartbeatTimer = 1f;
      }
    }
  }

  public void AddScore(int amount)
  {
    score += amount;
    UpdateUI();
    if (audioSource != null && collectSound != null)
      audioSource.PlayOneShot(collectSound);
  }

  public void TakeDamage(int amount)
  {
    currentHealth = Mathf.Max(0, currentHealth - amount);
    UpdateUI();
    if (audioSource != null && damageSound != null)
      audioSource.PlayOneShot(damageSound);
    if (currentHealth <= 0) GameOver();
  }

  private void UpdateUI()
  {
    if (scoreText != null) scoreText.text = $"Score: {score}";
    if (healthFill != null)
    {
      float healthPercent = (float)currentHealth / maxHealth;
      healthFill.fillAmount = healthPercent;

      Color healthColor;
      if (healthPercent > 0.6f)
        healthColor = Color.Lerp(Color.yellow, Color.green, (healthPercent - 0.6f) / 0.4f);
      else if (healthPercent > 0.3f)
        healthColor = Color.Lerp(Color.red, Color.yellow, (healthPercent - 0.3f) / 0.3f);
      else
        healthColor = Color.red;

      healthFill.color = healthColor;
    }
  }

  private void GameOver()
  {
    Debug.Log("GAME OVER!");
    if (audioSource != null && gameOverSound != null)
      audioSource.PlayOneShot(gameOverSound);
    if (gameOverPanel != null) gameOverPanel.SetActive(true);
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    Time.timeScale = 0f;
  }

  private void RestartGame()
  {
    Time.timeScale = 1f;
    UnityEngine.SceneManagement.SceneManager
    .LoadScene(UnityEngine.SceneManagement.SceneManager
    .GetActiveScene().buildIndex);
  }
}
