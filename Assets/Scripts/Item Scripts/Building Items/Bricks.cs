using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bricks : Building_Item
{

  public Bricks()
  {
    building_id = 6;

    id = 5;
    name = "Brix";
    description = "A deconstructed wall served with a caulk glaze.";
    tag = "Building Item";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/item_sack")};
    initial_sprite = sprites[0];

    Set_Phys_Rep();
    phys_rep.GetComponent<SpriteRenderer>().color = Color.red;
  }
}
