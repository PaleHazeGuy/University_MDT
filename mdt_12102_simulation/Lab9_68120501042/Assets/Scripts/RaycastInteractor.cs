using UnityEngine;
public class RaycastInteractor : MonoBehaviour
{
  [SerializeField] private float interactRange = 10f;
  [SerializeField] private LayerMask interactLayer;
  [SerializeField] private Color highlightColor = Color.yellow;
  private GameObject holdPoint;
  private GameObject currentTarget;
  private Color originalColor;
  private Renderer targetRenderer;
  private GameObject holding;


  void Start()
  {
    holdPoint = transform.Find("Main Camera/holdPoint").gameObject;
  }

  private void Update()
  {
    HandleHighlight();
    if (Input.GetMouseButtonDown(0) && currentTarget != null)
      InteractWith(currentTarget);
    if (Input.GetMouseButtonDown(1) && currentTarget != null)
      PushObject(currentTarget);
    if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
    {
      Holding(currentTarget);
    }
    if (Input.GetKeyUp(KeyCode.E) && holding != null)
    {
      Holding(null);
    }
  }
  private void HandleHighlight()
  {
    Ray ray = Camera.main.ScreenPointToRay(
    new Vector3(Screen.width / 2, Screen.height / 2, 0));
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
    {
      GameObject hitObj = hit.collider.gameObject;
      if (hitObj != currentTarget)
      {
        ResetHighlight();
        currentTarget = hitObj;
        targetRenderer = currentTarget.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
          originalColor = targetRenderer.material.color;
          targetRenderer.material.color = highlightColor;
        }
      }
    }
    else { ResetHighlight(); currentTarget = null; }
  }
  private void ResetHighlight()
  {
    if (targetRenderer != null) targetRenderer.material.color = originalColor;
    targetRenderer = null;
  }

  private void Holding(GameObject obj)
  {
    if (obj != null && holding == null)
    {
      holding = obj;
      Rigidbody rb = holding.GetComponent<Rigidbody>();
      if (rb != null) rb.isKinematic = true;
      holding.transform.SetParent(holdPoint.transform);
    }
    else if (holding != null)
    {
      Rigidbody rb = holding.GetComponent<Rigidbody>();
      if (rb != null) rb.isKinematic = false;
      holding.transform.SetParent(GameObject.Find("InteractiveObjects").transform);
      holding = null;
    }
  }

  private void InteractWith(GameObject obj)
  {
    DoorController door = obj.GetComponentInParent<DoorController>();
    if (door != null)
    {
      door.ToggleDoor();
      return;
    }

    ColorSwitcher colorSwitcher = obj.GetComponent<ColorSwitcher>();
    if (colorSwitcher != null)
    {
      colorSwitcher.SwitchColor();
      return;
    }

    Debug.Log($"Interact: {obj.name}");
    obj.transform.localScale = Vector3.one * Random.Range(0.5f, 2f);
  }

  private void PushObject(GameObject obj)
  {
    Rigidbody rb = obj.GetComponent<Rigidbody>();
    if (rb != null)
    {
      rb.AddForce((obj.transform.position - transform.position).normalized *
 8f, ForceMode.Impulse);
    }
  }
}
