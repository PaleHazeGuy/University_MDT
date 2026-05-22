using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0.1f;
    public float size = 1f;

    public int state = 0;

    public Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
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
    }
}
