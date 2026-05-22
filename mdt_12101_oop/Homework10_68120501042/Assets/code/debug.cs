using UnityEngine;

public class debug : MonoBehaviour
{
    public int hp;
    public float stamina;
    public string playerName;
    public bool mvp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    

        Debug.Log("Variable Start");
        Player1();
        

    }

    void Player1()
    {
        hp = 100;
        stamina = 75.5f;
        playerName = "Book";
        mvp = true;
        Debug.Log(hp + "," + stamina + "," + playerName + "," + mvp);
    }
    void Player2()
    {
        hp = 200;
        stamina = 75.6f;
        playerName = "mai";
        mvp = true;
        Debug.Log(hp + "," + stamina + "," + playerName + "," + mvp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
