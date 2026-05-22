using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableFunction : MonoBehaviour
{
  public string name;
  public int strength = 5;
  public int vitality = 4;
  int speed = 3;
  int luck = 2;
  int defend = 3;

  int hp;
  int stamina;
  // Start is called before the first frame update
  void Start()
  {
    this.enabled = false;

    CreatePlayer("Chicken", 10, 10, 3, 5, 8);
    CreatePlayer("Lama", 50, 50, 15, 10, 50);
  }

  void CreatePlayer(string name, int strength, int vitality, int speed, int luck, int defend)
  {
    stamina = 50 + (vitality * 5);
    hp = 100 + (stamina * strength);
    defend = 5 + (vitality * 2);

    Debug.Log(name + "," + hp + "," + stamina + "," + defend);
  }

  // Update is called once per frame
  void Update()
  {

  }
}

