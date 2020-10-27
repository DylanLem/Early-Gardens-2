using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Building_Item: Item
{

  //what building does this spawn?
  public int building_id;

  public Building_Item()
  {
  is_building = true;
  }

  public void Build()
  {
    GameObject temp_tile = phys_rep.transform.parent.gameObject;
    Delete();
    
    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Spawn_On_Tile(building_id,temp_tile);

  }


}
