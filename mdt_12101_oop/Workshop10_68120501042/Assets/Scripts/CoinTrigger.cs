using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
  static int coinValue = 0;
  private Rigidbody rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Ball"))
    {
      coinValue++;
      Debug.Log(coinValue);
      Destroy(gameObject);
    }
  }
  void OnTriggerStay(Collider other)
  {

  }
  void OnTriggerExit(Collider other)
  {

  }
}

