using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : Building_Item
{

  public Box()
  {
    building_id = 7;

    name = "Box";
    description = "A cardboard box.";
    tag = "Building Item";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("Buildings/cardboardbox")};
    initial_sprite = sprites[0];

    Set_Phys_Rep();
  }
}
