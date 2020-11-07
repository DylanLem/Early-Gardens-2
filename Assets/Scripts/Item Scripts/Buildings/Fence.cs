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


    id = 5;

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
  }

  public override void Update()
  {

  }


  private void Determine_Sprite()
  {

    string sprite_name = "buildings/fences/fence_";
    //For each neighbour, we add a corresponding character and build a string that represents a sprite
    //if that tile contains a building, add the value to our total
    if(Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)neighbours["Top_Mid"].GetComponent<tilebehavior>().Get_Grid_Pos().x,
                                                             (int)neighbours["Top_Mid"].GetComponent<tilebehavior>().Get_Grid_Pos().y].name == "Fence")
      sprite_name += "U";

    if(Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)neighbours["Mid_Right"].GetComponent<tilebehavior>().Get_Grid_Pos().x,
                                                             (int)neighbours["Mid_Right"].GetComponent<tilebehavior>().Get_Grid_Pos().y].name == "Fence")
     sprite_name += "R";

    if(Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)neighbours["Bot_Mid"].GetComponent<tilebehavior>().Get_Grid_Pos().x,
                                                             (int)neighbours["Bot_Mid"].GetComponent<tilebehavior>().Get_Grid_Pos().y].name == "Fence")
     sprite_name += "D";

    if(Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)neighbours["Mid_Left"].GetComponent<tilebehavior>().Get_Grid_Pos().x,
                                                             (int)neighbours["Mid_Left"].GetComponent<tilebehavior>().Get_Grid_Pos().y].name == "Fence")
     sprite_name += "L";

    //0 neighbour case
    if(sprite_name == "buildings/fences/fence_")
      Update_Sprite(Resources.Load<Sprite>("buildings/fences/fence_post"));

    else
      Update_Sprite(Resources.Load<Sprite>(sprite_name));
  }
}
