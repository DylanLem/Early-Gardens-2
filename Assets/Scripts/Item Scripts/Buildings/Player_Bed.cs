using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Bed : Building
{

  public Player_Bed() : base()
  {

    name = "Box";
    description = "It's a box.";
    tag = "Wall";


    sprites = new Sprite[]
    {Resources.Load<Sprite>("Buildings/bed_person")};

    Set_Phys_Rep();
  }

}
