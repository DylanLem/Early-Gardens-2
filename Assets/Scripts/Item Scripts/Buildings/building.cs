using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Building : Item
{


  //the neighbouring tiles. Helps with sprite logic and more
  public Dictionary<string,GameObject> neighbours;


  public Building()
  {
    is_building = true;
    is_pushable = false;


  }


  // If your building has some sort of autonomy, give it an Update() function
  // It gets called by the item manager every frame.
  public virtual void Update()
  {

  }


  protected void Set_Neighbours()
  {
    neighbours = new Dictionary<string,GameObject>()
    {
      {"Top-Left",null}, {"Top-Mid",null}, {"Top-Right",null},
      {"Mid-Left",null},/*Your building !*/{"Mid-Right",null},
      {"Bot-Left",null}, {"Bot-Mid",null}, {"Bot-Right",null}
    };
    var gridmanager = GameObject.FindWithTag("Grid");

    //increments along the entire loop.
    int dict_indexer = -1;

    for(int row = (int)grid_pos.y + 1; row >= grid_pos.y - 1; row--)
      for(int column = (int)grid_pos.x - 1; column <= grid_pos.x + 1;  column++)
      {
        //skips the tile the building is on
        if(new Vector3(column,row) == grid_pos) continue;

        dict_indexer += 1;

        var neighbour = gridmanager.GetComponent<gridmanager>().Get_Tile(new Vector3(column,row));

        if(neighbour == null) continue;
        neighbours[neighbours.ElementAt(dict_indexer).Key] = gridmanager.GetComponent<gridmanager>().Get_Tile(new Vector3(column,row));

      }

    Debug.Log(neighbours["Top-Mid"]);
    Debug.Log(neighbours["Mid-Right"]);
    Debug.Log(neighbours["Bot-Mid"]);
    Debug.Log(neighbours["Mid-Left"]);
  }


}
