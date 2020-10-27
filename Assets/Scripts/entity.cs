using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity
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

  /*  ***NOTES***

    To create a new entity subclass, you HAVE to define the following:
          name
          id
          tag
          Sprites[]

    Another MUST is calling Set_Phys_Rep() in the entity's initializer
  */


  public Entity()
  {
    current_sprite = 0;
  }


  public void Update_Sprite(Sprite s)
  {
    phys_rep.GetComponent<SpriteRenderer>().sprite = s;
    current_sprite += 1;
  }


  public void Delete()
  {

  }


  protected void Set_Phys_Rep()
  {
    Debug.Log(tag);
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






}
