using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
  [SerializeField] float cycleSpeed = 10f;
  void Start()
  {

  }

  void Update()
  {
    transform.Rotate(cycleSpeed * Time.deltaTime, 0, 0);
  }
}
