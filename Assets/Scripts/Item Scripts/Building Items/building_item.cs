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

  }

  public void Build()
  {
    GameObject target_tile = phys_rep.transform.parent.gameObject;
    Delete();

    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Spawn_On_Tile(building_id,target_tile);

  }


}
