using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
  public Color[] colors = new Color[]
 {
    Color.red,
    Color.green,
    Color.blue,
    Color.yellow,
    Color.cyan,
    Color.magenta,
    Color.white,
 };
  private int currentIndex = 0;

  void Start()
  {
    GetComponent<Renderer>().material.color = colors[0];
  }

  public void SwitchColor()
  {
    currentIndex = (currentIndex + 1) % colors.Length;
    GetComponent<Renderer>().material.color = colors[currentIndex];
  }
}
