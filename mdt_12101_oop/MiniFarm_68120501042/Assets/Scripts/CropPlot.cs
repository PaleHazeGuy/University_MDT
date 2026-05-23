using UnityEngine;
public class CropPlot : MonoBehaviour
{
  public enum PlotState { Empty, Planted, Watered, Grown }
  [SerializeField] private GameObject cropPrefab;
  [SerializeField] private float growDuration = 5f;
  [SerializeField] private Vector3 grownScale = new Vector3(0.8f, 1.5f, 0.8f);
  [SerializeField] private PlotState state = PlotState.Empty;
  private GameObject currentCrop;
  private float growTimer = 0f;
  private Vector3 seedScale = new Vector3(0.2f, 0.2f, 0.2f);
  private Renderer plotRenderer;
  private Color originalColor;
  private void Start()
  {
    plotRenderer = GetComponent<Renderer>();
    originalColor = plotRenderer.material.color;
  }
  private void Update()
  {
    if (state != PlotState.Watered || currentCrop == null) return;
    growTimer += Time.deltaTime;
    float progress = Mathf.Clamp01(growTimer / growDuration);
    currentCrop.transform.localScale = Vector3.Lerp(seedScale, grownScale,
   progress);
    if (progress >= 1f)
    {
      state = PlotState.Grown;
      plotRenderer.material.color = Color.green;
      Debug.Log($"{name}: พร้อมเก็บเ ก็ กี่ยว!");
    }
  }
  public bool TryPlant()
  {
    if (state != PlotState.Empty) return false;
    currentCrop = Instantiate(cropPrefab,
    transform.position + Vector3.up * 0.3f, Quaternion.identity);
    currentCrop.transform.localScale = seedScale;
    currentCrop.transform.parent = transform;
    state = PlotState.Planted;
    plotRenderer.material.color = new Color(0.6f, 0.4f, 0.2f);
    return true;
  }
  public bool TryWater()
  {
    if (state != PlotState.Planted) return false;
    state = PlotState.Watered;
    growTimer = 0f;
    plotRenderer.material.color = new Color(0.3f, 0.3f, 0.6f);
    return true;
  }
  public bool TryHarvest()
  {
    if (state != PlotState.Grown) return false;
    Destroy(currentCrop);
    currentCrop = null;
    state = PlotState.Empty;
    plotRenderer.material.color = originalColor;
    return true;
  }
}

