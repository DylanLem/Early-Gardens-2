using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fence : Building
{

  public GameObject Itemmanager;

  public Fence()
  {


    id = 8;

    name = "Fence";
    description = "Out or in, these will do it.";
    tag = "Wall";

    //This is pretty hardcoded right now...
    sprites = new Sprite[]
    {Resources.Load<Sprite>("buildings/fences/fence_U"), Resources.Load<Sprite>("buildings/fences/fence_R"),
     Resources.Load<Sprite>("buildings/fences/fence_D"), Resources.Load<Sprite>("buildings/fences/fence_L"),
     Resources.Load<Sprite>("buildings/fences/fence_UR"), Resources.Load<Sprite>("buildings/fences/fence_UD"),
     Resources.Load<Sprite>("buildings/fences/fence_UL"), Resources.Load<Sprite>("buildings/fences/fence_vert_RD"),
     Resources.Load<Sprite>("buildings/fences/fence_vert_RL"), Resources.Load<Sprite>("buildings/fences/fence_DL"),
     Resources.Load<Sprite>("buildings/fences/fence_URD"), Resources.Load<Sprite>("buildings/fences/fence_URL"),
     Resources.Load<Sprite>("buildings/fences/fence_UDL"), Resources.Load<Sprite>("buildings/fences/fence_RDL"),
     Resources.Load<Sprite>("buildings/fences/fence_URDL"), Resources.Load<Sprite>("buildings/fences/fence_post")
     };

    Itemmanager = GameObject.FindWithTag("Item Manager");

    Set_Phys_Rep();

    Set_Neighbours();
  }

  public override void Update()
  {
    Determine_Sprite();
  }


  private void Determine_Sprite()
  {

    string sprite_name = "fence_";
    //For each neighbour, we add a corresponding character and build a string that represents a sprite
    //if that tile contains a building, add the value to our total
    if(neighbours["Top_Mid"] != null && Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x,(int)grid_pos.y + 1]?.name == "Fence")
      sprite_name += "U";

    if(neighbours["Mid_Right"] != null && Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x + 1,(int)grid_pos.y]?.name == "Fence")
     sprite_name += "R";

    if(neighbours["Bot_Mid"] != null && Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x,(int)grid_pos.y - 1]?.name == "Fence")
     sprite_name += "D";

    if(neighbours["Mid_Left"] != null && Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x + 1,(int)grid_pos.y]?.name == "Fence")
     sprite_name += "L";

    //0 neighbour case
    if(sprite_name == "fence_")
      Update_Sprite(SpriteManager.FindSpriteFromSheet(SpriteManager.FenceSprites,"fence_post"));

    else
      Update_Sprite(SpriteManager.FindSpriteFromSheet(SpriteManager.FenceSprites,sprite_name));
  }
}
