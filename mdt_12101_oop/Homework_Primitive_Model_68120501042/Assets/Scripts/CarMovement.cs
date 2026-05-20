using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  void Update()
  {
    transform.position = new UnityEngine.Vector3(transform.position.x + (5f * Time.deltaTime), 0.55f, transform.position.z);
  }
}
