using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bed : Building_Item
{

  public Bed()
  {
    building_id = 12;

    name = "Bed";
    description = "A bed.";
    tag = "Building Item";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("Buildings/bed_person")};
    initial_sprite = sprites[0];

    Set_Phys_Rep();
  }
}
