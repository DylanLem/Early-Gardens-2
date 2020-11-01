using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item
{

  public int id{get;protected set;}
  public string tag{get;protected set;}
  public string name {get; set;}
  public string description {get;protected set;}

  public int value{get;protected set;}
  public int count{get;set;}

  public bool is_stackable = false;
  public bool is_pushable = true;
  public bool is_placed;

  public Vector3 grid_pos{get;set;}
  public Vector3 inv_pos{get;set;}
  public Sprite[] sprites {get;set;}
  public Sprite initial_sprite {get;set;}
  public int current_sprite {get;set;}

  public GameObject phys_rep {get; set;}


  public bool is_building = false;

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


  public void Update_Sprite(Sprite s)
  {
    phys_rep.GetComponent<SpriteRenderer>().sprite = s;
    current_sprite += 1;
  }




  protected void Set_Phys_Rep()
  {
    initial_sprite = sprites[0];
    phys_rep = new GameObject();
    phys_rep.AddComponent<SpriteRenderer>().sprite = initial_sprite;
    phys_rep.tag = tag;
  }


  protected void Set_Phys_Rep(Color color)
  {
    phys_rep = new GameObject();
    phys_rep.AddComponent<SpriteRenderer>().sprite = initial_sprite;
    phys_rep.GetComponent<SpriteRenderer>().color = color;
    phys_rep.tag = tag;
  }

  public void Delete()
  {
    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Delete_Item(this);
  }


  public void Become_Held(GameObject p_obj)
  {

    phys_rep.transform.parent = p_obj.transform;
    phys_rep.transform.position = p_obj.transform.position;

    GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().items_on_grid[(int)grid_pos.x,(int)grid_pos.y] = null;
    GameObject.FindWithTag("Grid").GetComponent<gridmanager>().grid[(int)grid_pos.x,(int)grid_pos.y].GetComponent<tilebehavior>().Remove_From_Tile();

    phys_rep.GetComponent<SpriteRenderer>().sprite = initial_sprite;
    phys_rep.GetComponent<SpriteRenderer>().sortingLayerName = "Grid";
    phys_rep.GetComponent<SpriteRenderer>().sortingOrder = 3;
  }

  public virtual Dictionary<string,dynamic> Pack_Data()
  {
    Dictionary<string,dynamic> item_data = new Dictionary<string,dynamic>()
    {
      {"id", id},
      {"count", count},
      {"current_sprite", current_sprite},
      {"grid_pos", new int[3]{(int)grid_pos.x,(int)grid_pos.y,(int)grid_pos.z}},
      {"inv_pos", new int[3]{(int)inv_pos.x,(int)inv_pos.y,(int)inv_pos.z}}
    };

    return item_data;
  }

  public void Load_Data(Dictionary<string,dynamic> item_data)
  {
    count = item_data["count"];
    current_sprite = item_data["current_sprite"];
    grid_pos = new Vector3(item_data["grid_pos"][0],item_data["grid_pos"][1],item_data["grid_pos"][2]);
    inv_pos = new Vector3(item_data["inv_pos"][0],item_data["inv_pos"][1],item_data["inv_pos"][2]);

    Update_Sprite(sprites[current_sprite]);
  }

}
