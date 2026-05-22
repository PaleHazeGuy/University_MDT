using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
  int framecount;
  // Start is called before the first frame update
  void Start()
  {
    //Console.WriteLine("Hello Unity from OOP Class");
    Debug.Log("Hello Unity from OOP Class");

    //transform.position = new Vector3(0f,1.0f,0f);
  }

  // Update is called once per frame
  void Update()
  {
    //framecount++;
    //Debug.Log($"Test Update : {framecount}");

    this.transform.Rotate(0f, 50f * Time.deltaTime, 0f); //25 deg/sec
  }
}

