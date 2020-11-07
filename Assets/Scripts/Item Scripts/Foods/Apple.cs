using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple: Food
{

  public Apple()
  {
    name = "Apple";
    description = "an apple for the little baby earl";
    tag = "Food";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/apple_0"),
     Resources.Load<Sprite>("items/apple_1"),
     Resources.Load<Sprite>("items/apple_2")};
    initial_sprite = sprites[0];


    Set_Phys_Rep();
  }
}
