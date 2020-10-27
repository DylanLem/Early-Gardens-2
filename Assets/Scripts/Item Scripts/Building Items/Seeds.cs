using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Seeds: Building_Item
{

  public Seeds()
  {
    building_id = 4;
    
    id = 3;
    name = "C-eed Packet";
    description = "Affordable. Renewable. Essential. The perfect start for an EARL Rancher.";
    tag = "Food";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/item_sack")};
    initial_sprite = sprites[0];

    Set_Phys_Rep();
  }

}
