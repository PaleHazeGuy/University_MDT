using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollision : MonoBehaviour
{
  private Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void OnCollisionEnter(Collision collision)
  {
    Debug.Log("Enter: " + collision.gameObject.name);

    if (collision.gameObject.CompareTag("Ball"))
    {
      Debug.Log("Player in Safe Zone");
    }
  }
  void OnCollisionStay(Collision collision)
  {
    Debug.Log("Stay: " + collision.gameObject.name);
  }
  void OnCollisionExit(Collision collision)
  {
    Debug.Log("Exit: " + collision.gameObject.name);
  }
}

