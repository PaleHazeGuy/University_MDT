using UnityEngine;
using UnityEngine.UI;
public class CrosshairController : MonoBehaviour
{
  [SerializeField] private Image crosshairImage;
  [SerializeField] private float interactRange = 10f;
  [SerializeField] private LayerMask interactLayer;
  [SerializeField] private Color normalColor = Color.white;
  [SerializeField] private Color hoverColor = Color.green;
  [SerializeField] private float hoverSize = 8f;
  [SerializeField] private float normalSize = 4f;
  private void Update()
  {
    Ray ray = Camera.main.ScreenPointToRay(
    new Vector3(Screen.width / 2, Screen.height / 2, 0));
    bool hit = Physics.Raycast(ray, interactRange, interactLayer);
    crosshairImage.color = hit ? hoverColor : normalColor;
    float size = hit ? hoverSize : normalSize;
    crosshairImage.rectTransform.sizeDelta = new Vector2(size, size);
  }
}
