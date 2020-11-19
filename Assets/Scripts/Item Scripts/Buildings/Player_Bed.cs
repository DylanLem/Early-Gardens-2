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


  public override void Interact(GameObject player)
  {
    Sleep_Player(player);
  }

  private void Sleep_Player(GameObject player)
  {



    GameObject.FindWithTag("Grid").GetComponent<gridmanager>().Update_Square(player.GetComponent<player>().playermanager.GetComponent<playermanager>().grid_pos,"remove");

    player.GetComponent<player>().grid_pos = grid_pos;
    player.GetComponent<player>().playermanager.GetComponent<playermanager>().grid_pos = grid_pos;


    player.transform.position = phys_rep.transform.position;
    player.transform.parent = phys_rep.transform;


    player.GetComponent<player>().Sleep();

    GameObject grid = GameObject.FindWithTag("Grid");
    GameObject earl_manager = GameObject.FindWithTag("Earl Manager");
    SaveSystem.Save_Game(grid, player, earl_manager);
  }

}
