using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food: Item
{

  public Dictionary<string,int> gains {get;protected set;}

  public Food()
  {
  }

  public Dictionary<string,int> Become_Eaten()
  {
    //returns a dictionary of things/status effects the earl grants.
    //handling sprite stuff and logic
    if(current_sprite < sprites.GetLength(0) - 1)
      Update_Sprite(sprites[current_sprite + 1]);
    else
      Delete();

    return gains;

  }

}
