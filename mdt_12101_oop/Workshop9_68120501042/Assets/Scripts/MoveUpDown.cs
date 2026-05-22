using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
  public float speed = 0.1f;
  public float size = 5f;

  public int state = 0;

  public Vector3 startPos;

  // Start is called before the first frame update
  void Start()
  {
    startPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (state == 0)
    {
      this.transform.Translate(Vector3.up * speed);
      if (transform.position.y >= startPos.y + size)
      {
        state = 1;
      }
    }
    else if (state == 1)
    {
      this.transform.Translate(Vector3.down * speed);
      if (transform.position.y <= startPos.y)
      {
        state = 0;
      }
    }
  }
}

