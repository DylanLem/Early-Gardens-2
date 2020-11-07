using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardboardBox : Building
{

  public CardboardBox() : base()
  {

    name = "Box";
    description = "It's a box.";
    tag = "Wall";


    sprites = new Sprite[]
    {Resources.Load<Sprite>("Buildings/cardboardbox")};

    Set_Phys_Rep();
  }

}
