using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fence : Building
{

  public Fence()
  {
    id = 5;

    name = "Fence";
    description = "Out or in, these will do it.";
    tag = "Wall";

    //This is pretty hardcoded right now...
    sprites = new Sprite[]
    {Resources.Load<Sprite>("buildings/fences/fence_corner_top"), Resources.Load<Sprite>("buildings/fences/fence_corner_bottom"),
     Resources.Load<Sprite>("buildings/fences/fence_horizontal"), Resources.Load<Sprite>("buildings/fences/fence_middle"),
     Resources.Load<Sprite>("buildings/fences/fence_middle_bottom"), Resources.Load<Sprite>("buildings/fences/fence_middle_side"),
     Resources.Load<Sprite>("buildings/fences/fence_middle_top"), Resources.Load<Sprite>("buildings/fences/fence_vert_post_bottom"),
     Resources.Load<Sprite>("buildings/fences/fence_vert_post_top"), Resources.Load<Sprite>("buildings/fences/fence_vertical"),
     Resources.Load<Sprite>("buildings/fences/fence_post")
     };

    Set_Phys_Rep();
  }

  public void Update()
  {

  }


  private void Determine_Sprite()
  {

    int sprite_decider = 0;
    //Assign an integer value to each neighbouring tile
    //if that tile contains a building, add the value to our total
    if(neighbours["Top_Mid"].GetComponent<tilebehavior>().Is_Empty()) sprite_decider += 1;
    if(neighbours["Mid_Right"].GetComponent<tilebehavior>().Is_Empty()) sprite_decider += 10;
    if(neighbours["Bot_Mid"].GetComponent<tilebehavior>().Is_Empty()) sprite_decider += 100;
    if(neighbours["Mid_Left"].GetComponent<tilebehavior>().Is_Empty()) sprite_decider += 1000;

    switch(sprite_decider)
    {
      case 0:
        Update_Sprite(sprites[0]);
        break;

      case 1:
        Update_Sprite(sprites[0]);
        break;

      case 10:
        Update_Sprite(sprites[0]);
        break;

      case 100:
        Update_Sprite(sprites[0]);
        break;

      case 1000:
        Update_Sprite(sprites[0]);
        break;

      case 11:
        Update_Sprite(sprites[0]);
        break;

      case 111:
        Update_Sprite(sprites[0]);
        break;

      case 1111:
        Update_Sprite(sprites[0]);
        break;

      case 1110:
        Update_Sprite(sprites[0]);
        break;

      case 1100:
        Update_Sprite(sprites[0]);
        break;

      case 1101:
        Update_Sprite(sprites[0]);
        break;

      case 1001:
        Update_Sprite(sprites[0]);
        break;

    }

  }
}
