using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
  public float speed = 0.01f;
  public float size = 3f;

  public Vector3 startPos;
  public int state = 0;

  // Start is called before the first frame update
  void Start()
  {
    startPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    //this.transform.position = new Vector3(3,0,0);
    //this.transform.position += new Vector3(0.01f, 0, 0);
    //this.transform.Translate(new Vector3(0.01f, 0, 0));
    //this.transform.Translate(speed * Vector3.forward);

    //this.transform.Rotate(0, 1*speed, 0);

    //this.transform.localScale = new Vector3(3, 3, 3);
    //this.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);

    /*
    if (state == 0)
    {      
        this.transform.Translate(Vector3.right * speed);
        if (transform.position.x >= startPos.x + size)
        {
            state = 1;
        }
    }
    else if (state == 1)
    {
        this.transform.Translate(Vector3.forward * speed);
        if (transform.position.z >= startPos.z + size)
        {
            state = 2;
        }
    }
    else if (state == 2)
    {
        this.transform.Translate(Vector3.left * speed);
        if (transform.position.x <= startPos.x)
        {
            state = 3;
        }
    }
    else if (state == 3)
    {
        this.transform.Translate(Vector3.back * speed);
        if (transform.position.z <= startPos.z)
        {
            state = 0;
        }
    }
    */
  }
}

