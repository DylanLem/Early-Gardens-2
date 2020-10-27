using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : Entity
{

  /*  ***NOTES***

    To create a new item subclass, you HAVE to define the following:
          name
          id
          tag
          Sprites[]

    Another MUST is calling Set_Phys_Rep() in the item's initializer
  */

  public Item()
  {

  }

  public void Delete()
  {
    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Delete_Item(this);
  }


  public void Become_Held(GameObject p_obj)
  {
    Debug.Log(p_obj.transform.position);
    phys_rep.transform.parent = p_obj.transform;
    phys_rep.transform.position = p_obj.transform.position;

    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x,(int)grid_pos.y] = null;
    GameObject.FindWithTag("Grid").GetComponent<gridmanager>().grid[(int)grid_pos.x,(int)grid_pos.y].GetComponent<tilebehavior>().Remove_From_Tile();

    phys_rep.GetComponent<SpriteRenderer>().sprite = initial_sprite;
    phys_rep.GetComponent<SpriteRenderer>().sortingLayerName = "Grid";
    phys_rep.GetComponent<SpriteRenderer>().sortingOrder = 3;
  }



}
