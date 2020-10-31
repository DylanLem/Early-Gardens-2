using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : Building
{

  public Wall() : base()
  {
    id = 6;

    name = "Wall";
    description = "It's bricks.";
    tag = "Wall";


    sprites = new Sprite[]
    {Resources.Load<Sprite>("Buildings/brick")};

    Set_Phys_Rep();
  }

}
