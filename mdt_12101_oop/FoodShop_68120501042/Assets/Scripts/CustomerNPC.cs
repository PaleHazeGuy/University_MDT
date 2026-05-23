using UnityEngine;
public class CustomerNPC : MonoBehaviour
{
  public enum CustState { Walking, Waiting, Served, Leaving }
  [SerializeField] private float walkSpeed = 3f;
  [SerializeField] private float patience = 15f;
  [HideInInspector] public CustState state = CustState.Walking;
  [HideInInspector] public string orderColor = "";
  private Transform waitPoint;
  private Transform exitPoint;
  private Renderer bodyRenderer;
  private Renderer orderIndicator;
  private float waitTimer = 0f;
  public void Setup(Transform wait, Transform exit, string order, Color c)
  {
    waitPoint = wait; exitPoint = exit;
    orderColor = order;
    bodyRenderer = GetComponent<Renderer>();
    // สร้าง Sphere child "แสดง Order" เหนือหัว
    Transform indicator = transform.Find("OrderBubble");
    if (indicator != null)
    {
      orderIndicator = indicator.GetComponent<Renderer>();
      orderIndicator.material.color = c;
    }
  }
  private void Update()
  {
    switch (state)
    {
      case CustState.Walking:
        transform.position = Vector3.MoveTowards(transform.position, waitPoint.position, walkSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waitPoint.position) <
       0.1f)
          state = CustState.Waiting;
        break;
      case CustState.Waiting:
        waitTimer += Time.deltaTime;
        // เปลี่ยนสีเมื่อใกล้หมล้ ดเวลา
        float ratio = waitTimer / patience;
        bodyRenderer.material.color =
        Color.Lerp(Color.white, Color.red, ratio);
        if (waitTimer >= patience)
        {
          Debug.Log("ลูกค้าโก ค้ รธ! เดินออก");
          ShopManager.Instance.CustomerLeft(false);
          state = CustState.Leaving;
        }
        break;
      case CustState.Served:
        bodyRenderer.material.color = Color.green;
        state = CustState.Leaving;
        break;
      case CustState.Leaving:
        Vector3 exit = exitPoint != null ?
        exitPoint.position : transform.position + Vector3.forward *
       10;
        transform.position = Vector3.MoveTowards(
        transform.position, exit, walkSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, exit) < 0.1f)
          Destroy(gameObject);
        break;
    }
  }
  public bool TryServe(string dish)
  {
    if (state != CustState.Waiting) return false;
    if (dish == orderColor)
    { state = CustState.Served; return true; }
    Debug.Log($"ผิด! ลูกค้าค้สั่งสั่ {orderColor} แต่ไต่ ด้ {dish}");
    return false;
  }
}

