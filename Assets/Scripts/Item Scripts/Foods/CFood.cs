using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CFood: Food
{

  public CFood()
  {
    name = "CFood";
    description = "Every EARL loves C-Food";
    tag = "Food";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/cfruit_0"),
     Resources.Load<Sprite>("items/cfruit_1"),
     Resources.Load<Sprite>("items/cfruit_2"),
     Resources.Load<Sprite>("items/cfruit_3")};
    initial_sprite = sprites[0];

    gains = new Dictionary<string,int>()
    {
      {"satiety", 15}
    };

    Set_Phys_Rep();
  }
}
