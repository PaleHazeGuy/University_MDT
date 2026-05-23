using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
  public float speed = 1.0f;

  void Update()
  {
    transform.Rotate(0, 1 * speed, 0);
  }
}

