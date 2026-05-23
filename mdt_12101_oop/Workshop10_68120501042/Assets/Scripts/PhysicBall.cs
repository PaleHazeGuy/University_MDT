using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicBall : MonoBehaviour
{
  public float forcePower = 1.0f;
  public float jumpPower = 5.0f;

  private Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    //transform.Translate(Vector3.forward*0.01f);
    //rb.AddForce(new Vector3(0.1f, 0f, 0f));
    //rb.AddForce(Vector3.up); //rb.AddForce(new Vector3(0f, 1f, 0f));
    if (Input.GetKeyDown(KeyCode.Space))   //Input.GetAxis("Jump")
    {
      rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
  }

  void FixedUpdate()
  {
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");
    //Debug.Log(moveX + " , " + moveZ);
    Vector3 movement = new Vector3(moveX, 0, moveZ);
    rb.AddForce(movement * forcePower);

  }
}

